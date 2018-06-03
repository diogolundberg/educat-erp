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
          <FinanceDataForm
            :data="enrollment.data.financeData"
            :errors="enrollment.errors.financeData"
            :options="enrollment.options"
            :messages="enrollment.messages.financeData"
            :underage="underage"
            @click="saveFinanceData" />
        </Step>
        <Step
          v-if="!enrollment.data.sentAt"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <EnrollmentSummary
            :summary="summary"
            :messages="enrollment.messages.sendToApproval"
            @submit="submitEnrollment" />
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
        appointmentDate: null,
        discriminators: [
          { id: "RepresentativePerson", name: "CPF" },
          { id: "RepresentativeCompany", name: "CNPJ" },
        ],
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
    },
  };
</script>
