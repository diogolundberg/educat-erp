<template>
  <div>
    <Header
      :notification-count="pendencies.length"
      notifications
      @notifications="notifications = !notifications">
      <div class="flex items-center">
        <Icon name="clock" />
        <div class="mx2 px2 border-white-50 border-left inline-block h6">
          Olá, <strong>{{ enrollment.data.personalData.realName }}</strong><br>
          <template v-if="enrollment.data.deadline">
            O seu processo de matrícula se encerra em
            {{ enrollment.data.daysRemaining }} dias.
          </template>
        </div>
      </div>
    </Header>
    <Notifications
      v-if="notifications"
      :items="pendencies"
      @click="notificationClick" />

    <Spinner :active="!enrollment.data.deadline">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          :complete="enrollment.data.personalData.status === 'valid'"
          :error="enrollment.data.personalData.status === 'invalid'"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <Card
            :complete="enrollment.data.personalData.status === 'valid'"
            :error="enrollment.data.personalData.status === 'invalid'"
            title="Seus Dados"
            @close="step = 0">
            <BaseErrors
              v-model="enrollment.messages.personalData" />
            <div
              slot="title"
              class="center mb4n mt2">
              <UploadZone
                v-model="enrollment.data.photo"
                prefix="picture"
                @input="savePhoto">
                <img
                  v-if="enrollment.data.photo"
                  :src="enrollment.data.photo"
                  class="rounded border4 border shadow2 x6 y6 bg-white">
                <img
                  v-if="!enrollment.data.photo"
                  src="../../assets/img/people.svg"
                  class="rounded border4 border shadow2 x6 y6 bg-white">
              </UploadZone>
            </div>
            <Fieldset id="personal">
              <InputBox
                v-model="enrollment.data.personalData.realName"
                :errors="enrollment.errors.personalData.realName"
                :size="6"
                required
                disabled
                label="Nome"
                hint="Seu nome completo" />
              <InputBox
                v-model="enrollment.data.personalData.assumedName"
                :errors="enrollment.errors.personalData.assumedName"
                :disabled="!enrollment.data.personalData.editable"
                :size="6"
                label="Nome Social"
                hint="Seu nome social" />
              <InputBox
                v-model="enrollment.data.personalData.cpf"
                :errors="enrollment.errors.personalData.cpf"
                :size="3"
                :min-size="14"
                required
                disabled
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="enrollment.data.personalData.birthDate"
                :errors="enrollment.errors.personalData.birthDate"
                :size="3"
                :min-size="10"
                :disabled="!enrollment.data.personalData.editable"
                date
                required
                label="Nascimento"
                mask="##/##/####"
                hint="Ex: 18/12/2001" />
              <DropDown
                v-model="enrollment.data.personalData.maritalStatusId"
                :errors="enrollment.errors.personalData.maritalStatusId"
                :size="3"
                :options="enrollment.options.maritalStatuses"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Estado Civil" />
              <DropDown
                v-model="enrollment.data.personalData.genderId"
                :errors="enrollment.errors.personalData.genderId"
                :size="3"
                :options="enrollment.options.genders"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Sexo" />
              <DropDown
                v-model="enrollment.data.personalData.nationalityId"
                :errors="enrollment.errors.personalData.nationalityId"
                :size="3"
                :options="enrollment.options.nationalities"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Nacionalidade" />
              <DropDown
                v-model="enrollment.data.personalData.birthCountryId"
                :errors="enrollment.errors.personalData.birthCountryId"
                :size="3"
                :options="enrollment.options.countries"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="País de Origem"
                hint="Ex: Brasil" />
              <template v-if="birthHasUF">
                <DropDown
                  v-model="enrollment.data.personalData.birthStateId"
                  :errors="enrollment.errors.personalData.birthStateId"
                  :size="3"
                  :options="enrollment.options.states"
                  :disabled="!enrollment.data.personalData.editable"
                  required
                  label="UF de Nascimento" />
                <DropDown
                  v-model="enrollment.data.personalData.birthCityId"
                  :errors="enrollment.errors.personalData.birthCityId"
                  :size="3"
                  :options="enrollment.options.cities"
                  :filter="enrollment.data.personalData.birthStateId"
                  :disabled="!enrollment.data.personalData.editable"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Naturalidade"
                  hint="Cidade de Nascimento" />
              </template>
              <template v-else>
                <InputBox
                  v-model="enrollment.data.personalData.birthStateName"
                  :errors="enrollment.errors.personalData.birthStateName"
                  :size="3"
                  :disabled="!enrollment.data.personalData.editable"
                  required
                  label="UF de Nascimento" />
                <InputBox
                  v-model="enrollment.data.personalData.birthCityName"
                  :errors="enrollment.errors.personalData.birthCityName"
                  :size="3"
                  :disabled="!enrollment.data.personalData.editable"
                  required
                  label="Naturalidade"
                  hint="Cidade de Nascimento" />
              </template>
              <InputBox
                v-model="enrollment.data.personalData.highSchoolGraduationYear"
                :errors="enrollment.errors.personalData.highSchoolGraduationYear"
                :size="6"
                :min-size="4"
                :max-size="4"
                :disabled="!enrollment.data.personalData.editable"
                :max-value="new Date().getFullYear()"
                mask="####"
                required
                label="Ano de conclusão do ensino médio"
                hint="Ex: 2017" />
              <DropDown
                v-model="enrollment.data.personalData.highSchoolGraduationCountryId"
                :errors="enrollment.errors.personalData.highSchoolGraduationCountryId"
                :size="6"
                :options="enrollment.options.countries"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="País de conclusão do ensino médio" />
            </Fieldset>
            <ContactBlock
              id="contact"
              v-model="enrollment.data.personalData"
              :errors="enrollment.errors.personalData"
              :disabled="!enrollment.data.personalData.editable"
              disable-email />
            <AddressBlock
              id="address"
              v-model="enrollment.data.personalData"
              :errors="enrollment.errors.personalData"
              :disabled="!enrollment.data.personalData.editable"
              :options="enrollment.options" />
            <Fieldset
              id="census"
              title="Dados para o Censo">
              <DropDown
                v-model="enrollment.data.personalData.raceId"
                :errors="enrollment.errors.personalData.raceId"
                :size="3"
                :options="enrollment.options.races"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Raça" />
              <DropDown
                v-model="enrollment.data.personalData.highSchoolKindId"
                :errors="enrollment.errors.personalData.highSchoolKindId"
                :size="3"
                :options="enrollment.options.highSchoolKinds"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Escola" />
              <InputBox
                v-model="enrollment.data.personalData.mothersName"
                :errors="enrollment.errors.personalData.mothersName"
                :size="6"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Nome completo da mãe" />
            </Fieldset>
            <Fieldset
              id="other"
              title="Outras Informações">
              <RadioGroup
                v-model="enrollment.data.personalData.handicap"
                :errors="enrollment.errors.personalData.handicap"
                :options="handicap"
                :disabled="!enrollment.data.personalData.editable"
                required
                label="Possui alguma Deficiência, Transtorno Global do
                  Desenvolvimento, ou Habilidades/Superdotação?" />
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              id="specialNeeds"
              title="Selecione">
              <CheckGroup
                v-model="enrollment.data.personalData.disabilities"
                :errors="enrollment.errors.personalData.disabilities"
                :options="enrollment.options.disabilities"
                :disabled="!enrollment.data.personalData.editable" />
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <CheckGroup
                v-model="enrollment.data.personalData.specialNeeds"
                :errors="enrollment.errors.personalData.specialNeeds"
                :options="enrollment.options.specialNeeds"
                :disabled="!enrollment.data.personalData.editable"
                :filter="enrollment.data.personalData.disabilities"
                filter-key="disabilityId" />
            </Fieldset>
            <Fieldset
              id="documents"
              title="Documentos">
              <Documents
                v-model="enrollment.data.personalData.documents"
                :types="enrollment.options.personalDocuments"
                :errors="enrollment.errors.personalData.documents"
                :prefix="`onboarding/enrollment/${ id }/personalData/`"
                :disabled="!enrollment.data.personalData.editable"
                :validations="validations" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!enrollment.data.personalData.editable"
                primary
                label="Próximo"
                @click="savePersonalData" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="enrollment.data.financeData.status == 'valid'"
          :error="enrollment.data.financeData.status == 'invalid'"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <Card
            :complete="enrollment.data.financeData.status === 'valid'"
            :error="enrollment.data.financeData.status === 'invalid'"
            title="Dados Financeiros">
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
          </Card>
        </Step>
        <Step
          :complete="!!enrollment.data.sentAt"
          title="Informações da Matrícula"
          description="Saiba mais sobre seu curso">
          <Card
            title="Informações da Matrícula">
            <Fieldset title="Informações Gerais">
              <InfoBox
                label="Curso"
                value="Medicina" />
              <InfoBox
                label="Turno"
                value="Integral" />
              <InfoBox
                label="Forma de Ingresso"
                value="Vestibular" />
              <InfoBox
                label="Data de Ingresso"
                value="01/02/2018" />
            </Fieldset>
            <Fieldset title="Matriz Curricular">
              <List
                :show-filter="false"
                :per-page="40"
                :columns="[
                  {name:'series', title: 'Série'},
                  {name:'class', title: 'Disciplinas', css: 'col-5'},
                  {name:'theory', title: 'Teórica'},
                  {name:'practice', title: 'Prática'},
                  {name:'total', title: 'Total'},
                ]"
                :value="[
                  {series:'1',class:'Anatomia I',theory:80,practice:80,total:160},
                  {series:'1',class:'Anatomia II',theory:80,practice:80,total:160},
                  {series:'1',class:'Biofísica I',theory:40,practice:'-',total:40},
                  {series:'1',class:'Biofísica II',theory:72,practice:'-',total:72},
                  {series:'1',class:'Bioquímica I',theory:80,practice:'-',total:80},
                  {series:'1',class:'Bioquímica II',theory:40,practice:40,total:80},
                  {series:'1',class:'Citologia',theory:80,practice:80,total:160},
                  {series:'1',class:'Embriologia Humana',theory:80,practice:80,total:160},
                  {series:'1',class:'Genética',theory:72,practice:72,total:144},
                  {series:'1',class:'Histologia I',theory:40,practice:40,total:80},
                  {series:'1',class:'Histologia I',theory:40,practice:40,total:80},
                  {series:'1',class:'História da Medicina I',theory:72,practice:72,
                   total:144},
                  {series:'1',class:'Medicina Preventiva I',theory:80,practice:80,
                   total:160},
                  {series:'1',class:'Metodologia Científica',theory:80,practice:80,
                   total:160},
                  {series:'1',class:'Práticas em Saúde Coletiva',theory:72,practice:72,
                   total:144},
              ]" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!!enrollment.data.sentAt"
                primary
                label="Próximo"
                @click="step = 4" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="!enrollment.data.sentAt"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <Card
            title="Enviar para Análise">
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
          </Card>
        </Step>
        <Step
          v-if="enrollment.data.sentAt"
          complete
          title="Aguardando Aprovação"
          description="A secretaria e o departamento financeiro estão analisando
            seus documentos">
          <Card title="Dados enviados">
            <BaseErrors
              v-model="enrollment.messages.sendToApproval"
              success />
            <p>Seus dados foram enviados. Agora a secretaria e o departamento
            financeiro estão analisando seus documentos.</p>
            <div class="center">
              <Animation
                name="success" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="enrollment.data.academicApproval.status === 'approved'"
          :warning="enrollment.data.academicApproval.status === 'pending'"
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <Card
            v-if="enrollment.data.academicApproval.status === 'inReview'"
            title="Aprovação da Secretaria">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval.status === 'approved'"
            title="Matrícula Aprovada pela Secretaria">
            A secretaria já aprovou sua matrícula.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval.status === 'pending'"
            title="Matrícula Reprovada pela Secretaria">
            A secretaria solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="deleteAcademicPendencies" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="enrollment.data.financeApproval.status === 'approved'"
          :warning="enrollment.data.financeApproval.status === 'pending'"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <Card
            v-if="enrollment.data.financeApproval.status === 'inReview'"
            title="Aprovação do Financeiro">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.financeApproval.status === 'approved'"
            title="Matrícula Aprovada pelo Financeiro">
            Nosso departamento financeiro já aprovou sua matrícula.
          </Card>
          <Card
            v-if="enrollment.data.financeApproval.status === 'pending'"
            title="Matrícula Reprovada pelo Financeiro">
            Nosso departamento financeiro solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="deleteFinancePendencies" />
            </div>
          </Card>
        </Step>
        <Step
          title="Agende uma Visita"
          description="Após a aprovação da secretaria e do financeiro, é hora
            de agendar um horário para comparecer na CMMG." />
        <Step
          title="Matrícula Concluída!"
          description="Sua matrícula foi concluída!" />
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
        notifications: false,
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
      goToStep() {
        if (this.enrollment.data.personalData.status === "valid") {
          this.step = 2;
        }
        if (this.enrollment.data.financeData.status === "valid") {
          this.step = 3;
        }
        if (this.enrollment.data.sentAt != null) {
          this.step = null;
        }
      },
      focusOnErrors() {
        const firstError = this.$el.querySelector(".error");
        if (firstError) {
          firstError.focus();
        }
      },
      async savePhoto() {
        const token = this.id;
        const { photo } = this.enrollment.data;
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
