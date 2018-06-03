<template>
  <div>
    <Header
      :notifications="pendencies"
      @notification="notificationClick" />

    <EnrollmentWelcome
      v-if="enrollment.data.firstTime"
      :enrollment="enrollment.data"
      @done="startEnrollment" />

    <Spinner
      v-if="!enrollment.data.firstTime"
      :active="!enrollment.data.deadline">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          :complete="enrollment.data.personalData.status === 'valid'"
          :error="enrollment.data.personalData.status === 'invalid'"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <PersonalDataForm
            :data="enrollment.data.personalData"
            :errors="enrollment.errors.personalData"
            :options="enrollment.options"
            :messages="enrollment.messages.personalData"
            :photo="enrollment.data.photo"
            @photo="savePhoto"
            @submit="savePersonalData" />
        </Step>
        <Step
          :complete="!!enrollment.data.sentAt"
          title="Informações da Matrícula"
          description="Saiba mais sobre seu curso">
          <InfoForm
            @click="step = 3" />
        </Step>
        <Step
          :complete="enrollment.data.financeData.status == 'valid'"
          :error="enrollment.data.financeData.status == 'invalid'"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <div>
            <BaseErrors
              v-model="enrollment.messages.financeData" />
            <Fieldset title="Dados Financeiros">
              <List
                v-model="enrollment.options.plans"
                :columns="[
                  {name: 'name', title: 'Plano'},
                  {name: 'value', title: 'Total', css:'right-align'},
                  {name: 'installments', title: 'Parcelas', css:'right-align'},
                  {name: 'installmentValue', title: 'Valor da Parcela'},
                  {name: 'dueDate', title: 'Vencimento'},
                  {name: 'guarantors', title: 'Fiadores'},
                ]"
                :show-filter="false"
                :disabled="!enrollment.data.financeData.editable"
                @click="enrollment.data.financeData.planId = $event.id">
                <template
                  slot="column-name"
                  scope="{ row }">
                  <Radio
                    :value="enrollment.data.financeData.planId === row.id"
                    class="mr2" />
                  {{ row.name }}
                </template>
              </List>
              <DropDown
                v-model="enrollment.data.financeData.paymentMethodId"
                :errors="enrollment.errors.financeData.paymentMethodId"
                :size="6"
                :options="enrollment.options.paymentMethod"
                :disabled="!enrollment.data.financeData.editable"
                label="Meio de Pagamento" />
            </Fieldset>
            <Fieldset title="Responsável Financeiro">
              <InputBox
                v-model="enrollment.data.financeData.representative.cpf"
                :errors="enrollment.errors.financeData.representative &&
                enrollment.errors.financeData.representative.cpf"
                :size="4"
                :min-size="14"
                :disabled="!enrollment.data.financeData.editable || !underage"
                cpf
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="enrollment.data.financeData.representative.name"
                :errors="enrollment.errors.financeData.representative &&
                enrollment.errors.financeData.representative.name"
                :size="4"
                :disabled="!enrollment.data.financeData.editable || !underage"
                label="Nome completo" />
              <DropDown
                v-model="enrollment.data.financeData.representative.relationshipId"
                :errors="enrollment.errors.financeData.representative &&
                enrollment.errors.financeData.representative.relationshipId"
                :size="4"
                :options="enrollment.options.relationships"
                :disabled="!enrollment.data.financeData.editable || !underage"
                label="Relacionamento com o aluno" />
            </Fieldset>
            <ContactBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :disabled="!enrollment.data.financeData.editable || !underage" />
            <AddressBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :options="enrollment.options"
              :disabled="!enrollment.data.financeData.editable || !underage" />
            <Fieldset
              v-show="guarantorsAmount > 0"
              title="Fiadores">
              <Many
                v-model="enrollment.data.financeData.guarantors"
                :errors="enrollment.errors.financeData"
                :default="emptyGuarantor"
                :disabled="!enrollment.data.financeData.editable"
                :amount="guarantorsAmount"
                error-key="guarantors">
                <template slot-scope="{ item, error }">
                  <Fieldset
                    title="Dados Gerais">
                    <InputBox
                      v-model="item.cpf"
                      :errors="error.cpf"
                      :size="4"
                      :min-size="14"
                      :disabled="!enrollment.data.financeData.editable"
                      cpf
                      label="CPF"
                      mask="###.###.###-##"
                      hint="Ex: 000.000.000-00" />
                    <InputBox
                      v-model="item.name"
                      :errors="error.name"
                      :size="4"
                      :disabled="!enrollment.data.financeData.editable"
                      label="Nome" />
                    <DropDown
                      v-model="item.relationshipId"
                      :errors="error.relationshipId"
                      :size="4"
                      :options="enrollment.options.relationships"
                      :disabled="!enrollment.data.financeData.editable"
                      label="Relacionamento com o aluno" />
                  </FieldSet>
                  <AddressBlock
                    v-model="item"
                    :errors="error"
                    :disabled="!enrollment.data.financeData.editable"
                    :options="enrollment.options" />
                  <ContactBlock
                    v-model="item"
                    :errors="error"
                    :disabled="!enrollment.data.financeData.editable" />
                  <Documents
                    v-model="item.documents"
                    :errors="error.documents"
                    :types="enrollment.options.guarantorDocuments"
                    :prefix="`onboarding/enrollment/${ id }/financeData/`"
                    :disabled="!enrollment.data.financeData.editable"
                    :validations="validationsFor(item)" />
                </template>
              </Many>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!enrollment.data.financeData.editable"
                primary
                label="Próximo"
                @click="saveFinanceData" />
            </div>
          </div>
        </Step>
        <Step
          v-if="!enrollment.data.sentAt"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <div>
            <BaseErrors
              v-model="enrollment.messages.sendToApproval" />
            <p>Envie seus dados para a secetaria e para o departamento financeiro para
            aprovação.</p>
            <div class="center">
              <img
                :style="{ 'max-width': '8rem' }"
                src="../../assets/img/card.svg">
            </div>
            <EnrollmentSummary :summary="summary" />
            <Fieldset title="Confirmação">
              <Checkbox
                v-model="agreed"
                class="mb3"
                label="Confirmo que as informações acima estão corretas" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!agreed"
                primary
                label="Enviar"
                @click="submitEnrollment" />
            </div>
          </div>
        </Step>
        <Step
          v-if="enrollment.data.sentAt"
          complete
          title="Aguardando Aprovação"
          description="A secretaria e o departamento financeiro estão analisando
            seus documentos">
          <div>
            <BaseErrors
              v-model="enrollment.messages.sendToApproval"
              success />
            <p>Seus dados foram enviados. Agora a secretaria e o departamento
            financeiro estão analisando seus documentos.</p>
            <div class="center">
              <Animation
                name="success" />
            </div>
          </div>
        </Step>
        <Step
          :complete="enrollment.data.academicApproval.status === 'approved'"
          :warning="enrollment.data.academicApproval.status === 'pending'"
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <div
            v-if="enrollment.data.academicApproval.status === 'inReview'">
            Sua aprovação ainda está pendente.
          </div>
          <div
            v-if="enrollment.data.academicApproval.status === 'approved'">
            A secretaria já aprovou sua matrícula.
          </div>
          <div
            v-if="enrollment.data.academicApproval.status === 'pending'">
            A secretaria solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="deleteAcademicPendencies" />
            </div>
          </div>
        </Step>
        <Step
          :complete="enrollment.data.financeApproval.status === 'approved'"
          :warning="enrollment.data.financeApproval.status === 'pending'"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <div
            v-if="enrollment.data.financeApproval.status === 'inReview'">
            Sua aprovação ainda está pendente.
          </div>
          <div
            v-if="enrollment.data.financeApproval.status === 'approved'">
            Nosso departamento financeiro já aprovou sua matrícula.
          </div>
          <div
            v-if="enrollment.data.financeApproval.status === 'pending'">
            Nosso departamento financeiro solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="deleteFinancePendencies" />
            </div>
          </div>
        </Step>
        <Step
          :complete="enrollment.data.finished"
          title="Concluir Matrícula"
          description="Conclua sua matrícula.">
          <div
            v-if="enrollment.data.academicApproval.status === 'approved'
            && enrollment.data.financeApproval.status === 'approved'">
            Falta pouco! Clique no botão abaixo para concluir sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Concluir"
                @click="finishEnrollment" />
            </div>
          </div>
          <div
            v-else>
            Sua matrícula ainda não foi aprovada! Preencha corretamente os dados
            antes de concluir!
          </div>
        </Step>
        <Step
          title="Matrícula Concluída!"
          description="Após aprovação da secretaria e financeiro,
          e agendamento da visita, sua matrícula estará concluída.">
          <div>
            Após aprovação da secretaria e financeiro,
            e agendamento da visita, sua matrícula estará concluída.
          </div>
        </Step>
      </Stepper>
    </Spinner>
  </div>
</template>

<script>
  export default {
    name: "Enrollment",
    props: {
      id: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        step: 1,
        agreed: false,
        appointmentDate: null,
        handicap: [
          { id: "yes", name: "Sim" },
          { id: "no", name: "Não" },
          { id: "unknown", name: "Não disponho da informação" },
        ],
        discriminators: [
          { id: "RepresentativePerson", name: "CPF" },
          { id: "RepresentativeCompany", name: "CNPJ" },
        ],
        emptyGuarantor: {
          discriminator: "RepresentativePerson",
          cpf: "",
          cnpj: "",
          name: "",
          contact: "",
          relationshipId: null,
          streetAddress: "",
          complementAddress: "",
          neighborhood: "",
          phoneNumber: "",
          landline: "",
          email: "",
          cityId: null,
          stateId: null,
        },
      };
    },
    computed: {
      enrollment() {
        return this.$store.state.enrollment;
      },
      summary() {
        return this.$store.state.enrollmentSummary;
      },
      underage() {
        return this.enrollment.underage;
      },
      guarantorsAmount() {
        const { planId } = this.enrollment.data.financeData;
        const plan = this.enrollment.options.plans.find(a => a.id === planId);
        return plan && plan.guarantors;
      },
      validations() {
        const matches = (opt, key, val) =>
          this.enrollment.options[opt].filter(a => a[key]).map(a => a.id).includes(val);

        const isNative = matches(
          "nationalities",
          "checkNative",
          this.enrollment.data.personalData.nationalityId,
        );
        const isForeigner = matches(
          "nationalities",
          "checkForeign",
          this.enrollment.data.personalData.nationalityId,
        );
        const hasDraft = matches(
          "genders",
          "checkMilitaryDraft",
          this.enrollment.data.personalData.genderId,
        );
        const isSpouse = matches(
          "relationships",
          "checkSpouse",
          this.enrollment.data.financeData.representative.relationshipId,
        );
        const foreignGraduation = matches(
          "countries",
          "checkForeignGraduation",
          this.enrollment.data.personalData.highSchoolGraduationCountryId,
        );
        const gradutionCurrentYear = this.enrollment.data.onboardingYear ===
          this.enrollment.data.personalData.highSchoolGraduationYear;

        return [
          "MinorAge",
          isNative && "Native",
          isForeigner && "Foreigner",
          hasDraft && "MilitaryDraft",
          isSpouse && "Spouse",
          foreignGraduation && "ForeignGraduation",
          gradutionCurrentYear && "GraduationYear",
          !gradutionCurrentYear && "NotGraduationYear",
        ].filter(a => a);
      },
      pendencies() {
        return [...this.enrollment.data.academicApproval.pendencies || [],
                ...this.enrollment.data.financeApproval.pendencies || []];
      },
      birthHasUF() {
        const birthCountry = this.enrollment.options.countries.find(a =>
          a.id === this.enrollment.data.personalData.birthCountryId);
        return birthCountry && birthCountry.hasUF;
      },
    },
    watch: {
      "enrollment.data.personalData": {
        deep: true,
        handler() {
          this.$store.dispatch("copyResponsibleData");
        },
      },
      async step(value) {
        if (value === 4) {
          await this.$store.dispatch("getEnrollmentSummary", this.id);
        } else if (value === 8) {
          await this.$store.dispatch("getAppointments", this.id);
        }
      },
    },
    async mounted() {
      try {
        await this.$store.dispatch("getEnrollment", this.id);
        this.goToStep();
      } catch (ex) {
        this.$router.push("/404");
      }
    },
    methods: {
      async startEnrollment() {
        await this.$store.dispatch("submitWelcomePage", this.id);
      },
      goToStep() {
        if (this.enrollment.data.personalData.status === "valid") {
          this.step = 2;
        }
        if (this.enrollment.data.financeData.status === "valid") {
          this.step = 4;
        }
        if (this.enrollment.data.sentAt != null) {
          this.step = null;
        }
        if (this.enrollment.data.academicApproval.status === "approved" &&
          this.enrollment.data.financeApproval.status === "approved") {
            this.step = 7;
          }
        if (this.enrollment.data.finished) {
          this.step = 8;
        }
      },
      focusOnErrors() {
        const firstError = this.$el.querySelector(".error");
        if (firstError) {
          firstError.focus();
        }
      },
      async savePhoto(photo) {
        const token = this.id;
        await this.$store.dispatch("setEnrollmentAvatar", { token, photo });
      },
      async savePersonalData() {
        const token = this.id;
        const data = this.enrollment.data.personalData;
        await this.$store.dispatch("setPersonalData", { token, data });
        this.goToStep();
        this.focusOnErrors();
      },
      async saveFinanceData() {
        const token = this.id;
        const data = this.enrollment.data.financeData;
        data.representative.discriminator = "RepresentativePerson";
        await this.$store.dispatch("setFinanceData", { token, data });
        this.goToStep();
        this.focusOnErrors();
      },
      async submitEnrollment() {
        const token = this.id;
        await this.$store.dispatch("submitEnrollment", { token });
      },
      async deleteAcademicPendencies() {
        const token = this.id;
        await this.$store.dispatch("deleteAcademicPendencies", { token });
        this.step = null;
        this.notify("Sua matrícula foi enviada para aprovação.");
      },
      async deleteFinancePendencies() {
        const token = this.id;
        await this.$store.dispatch("deleteFinancePendencies", { token });
        this.step = null;
        this.notify("Sua matrícula foi enviada para aprovação.");
      },
      async finishEnrollment() {
        const token = this.id;
        await this.$store.dispatch("finishEnrollment", { token });
        this.step = 8;
      },
      async setAppointment() {
        const token = this.id;
        const id = this.appointmentDate;
        await this.$store.dispatch("setAppointment", { token, id });
        this.step = 9;
      },
      async notificationClick(anchor) {
        this.step = 1;
        await this.tick();
        await this.sleep(100);
        window.location.hash = anchor;
        this.notifications = false;
      },
      validationsFor(item) {
        const matches = (opt, key, val) =>
          this.enrollment.options[opt].filter(a => a[key]).map(a => a.id).includes(val);

        const isSpouse = matches(
          "relationships",
          "checkSpouse",
          item.relationshipId,
        );

        return [
          isSpouse && "Spouse",
        ];
      },
    },
  };
</script>
