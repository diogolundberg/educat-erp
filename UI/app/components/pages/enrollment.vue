<template>
  <div>
    <Header
      notifications
      @notifications="notifications = !notifications">
      <div class="flex items-center">
        <Icon name="clock" />
        <div class="mx2 px2 border-white-50 border-left inline-block h6">
          Olá, <strong>{{ enrollment.data.personalData.realName }}</strong><br>
          <template v-if="enrollment.data.deadline">
            O seu processo de matrícula se encerra em {{ daysRemaining }} dias.
          </template>
        </div>
      </div>
    </Header>
    <Notifications
      v-if="notifications"
      :items="enrollment.data.pendencies"
      @click="notifications = false" />

    <Spinner :active="!enrollment.data.deadline">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          :complete="enrollment.data.personalData.state === 'valid'"
          :error="enrollment.data.personalData.state === 'invalid'"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <Card
            :complete="enrollment.data.personalData.state === 'valid'"
            :error="enrollment.data.personalData.state === 'invalid'"
            title="Seus Dados"
            closeable
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
            <Fieldset>
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
                :disabled="!!enrollment.data.sentAt"
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
                :disabled="!!enrollment.data.sentAt"
                required
                label="Nascimento"
                mask="##/##/####"
                hint="Ex: 18/12/2001" />
              <DropDown
                v-model="enrollment.data.personalData.maritalStatusId"
                :errors="enrollment.errors.personalData.maritalStatusId"
                :size="3"
                :options="enrollment.options.maritalStatuses"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Estado Civil" />
              <DropDown
                v-model="enrollment.data.personalData.genderId"
                :errors="enrollment.errors.personalData.genderId"
                :size="3"
                :options="enrollment.options.genders"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Sexo" />
              <DropDown
                v-model="enrollment.data.personalData.nationalityId"
                :errors="enrollment.errors.personalData.nationalityId"
                :size="3"
                :options="enrollment.options.nationalities"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Nacionalidade" />
              <DropDown
                v-model="enrollment.data.personalData.birthCountryId"
                :errors="enrollment.errors.personalData.birthCountryId"
                :size="3"
                :options="enrollment.options.countries"
                :disabled="!!enrollment.data.sentAt"
                required
                label="País de Origem"
                hint="Ex: Brasil" />
              <DropDown
                v-model="enrollment.data.personalData.birthStateId"
                :errors="enrollment.errors.personalData.birthStateId"
                :size="3"
                :options="enrollment.options.states"
                :disabled="!!enrollment.data.sentAt"
                required
                label="UF de Nascimento" />
              <DropDown
                v-model="enrollment.data.personalData.birthCityId"
                :errors="enrollment.errors.personalData.birthCityId"
                :size="3"
                :options="enrollment.options.cities"
                :filter="enrollment.data.personalData.birthStateId"
                :disabled="!!enrollment.data.sentAt"
                filter-key="stateId"
                key-id="name"
                required
                label="Naturalidade"
                hint="Cidade de Nascimento" />
              <InputBox
                v-model="enrollment.data.personalData.highSchoolGraduationYear"
                :errors="enrollment.errors.personalData.highSchoolGraduationYear"
                :size="6"
                :min-size="4"
                :max-size="4"
                :disabled="!!enrollment.data.sentAt"
                mask="####"
                required
                label="Ano de conclusão do ensino médio"
                hint="Ex: 2017" />
              <DropDown
                v-model="enrollment.data.personalData.highSchoolGraduationCountryId"
                :errors="enrollment.errors.personalData.highSchoolGraduationCountryId"
                :size="6"
                :options="enrollment.options.countries"
                :disabled="!!enrollment.data.sentAt"
                required
                label="País de conclusão do ensino médio" />
            </Fieldset>
            <ContactBlock
              v-model="enrollment.data.personalData"
              :errors="enrollment.errors.personalData"
              :disabled="!!enrollment.data.sentAt"
              disable-email />
            <AddressBlock
              v-model="enrollment.data.personalData"
              :errors="enrollment.errors.personalData"
              :disabled="!!enrollment.data.sentAt"
              :options="enrollment.options" />
            <Fieldset title="Dados para o Censo">
              <DropDown
                v-model="enrollment.data.personalData.raceId"
                :errors="enrollment.errors.personalData.raceId"
                :size="3"
                :options="enrollment.options.races"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Raça" />
              <DropDown
                v-model="enrollment.data.personalData.highSchoolKindId"
                :errors="enrollment.errors.personalData.highSchoolKindId"
                :size="3"
                :options="enrollment.options.highSchoolKinds"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Escola" />
              <InputBox
                v-model="enrollment.data.personalData.mothersName"
                :errors="enrollment.errors.personalData.mothersName"
                :size="6"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Nome completo da mãe" />
            </Fieldset>
            <Fieldset title="Outras Informações">
              <RadioGroup
                v-model="enrollment.data.personalData.handicap"
                :errors="enrollment.errors.personalData.handicap"
                :options="handicap"
                :disabled="!!enrollment.data.sentAt"
                required
                label="Possui alguma Deficiência, Transtorno Global do
                  Desenvolvimento, ou Habilidades/Superdotação?" />
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Selecione">
              <CheckGroup
                v-model="enrollment.data.personalData.disabilities"
                :errors="enrollment.errors.personalData.disabilities"
                :options="enrollment.options.disabilities"
                :disabled="!!enrollment.data.sentAt" />
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <CheckGroup
                v-model="enrollment.data.personalData.specialNeeds"
                :errors="enrollment.errors.personalData.specialNeeds"
                :options="enrollment.options.specialNeeds"
                :disabled="!!enrollment.data.sentAt"
                :filter="enrollment.data.personalData.disabilities"
                filter-key="disabilityId" />
            </Fieldset>
            <Fieldset title="Documentos">
              <Documents
                v-model="enrollment.data.personalData.documents"
                :types="enrollment.options.personalDocuments"
                :errors="enrollment.errors.personalData.documents"
                :prefix="`onboarding/enrollment/${ id }/personalData/`"
                :disabled="!!enrollment.data.sentAt"
                :validations="validations" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!!enrollment.data.sentAt"
                primary
                label="Próximo"
                @click="savePersonalData" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="enrollment.data.financeData.state == 'valid'"
          :error="enrollment.data.financeData.state == 'invalid'"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <Card
            :complete="enrollment.data.financeData.state === 'valid'"
            :error="enrollment.data.financeData.state === 'invalid'"
            title="Dados Financeiros">
            <BaseErrors
              v-model="enrollment.messages.financeData" />
            <Fieldset title="Dados Financeiros">
              <DropDown
                v-model="enrollment.data.financeData.paymentMethodId"
                :errors="enrollment.errors.financeData.paymentMethodId"
                :size="6"
                :options="enrollment.options.paymentMethod"
                :disabled="!!enrollment.data.sentAt"
                label="Meio de Pagamento" />
              <List
                v-model="enrollment.options.plans"
                :columns="[
                  {name: 'name', title: 'Plano'},
                  {name: 'value', title: 'Total'},
                  {name: 'installments', title: 'Parcelas'},
                  {name: 'dueData', title: 'Vencimento'},
                  {name: 'guarantors', title: 'Fiadores'},
                ]"
                :show-filter="false"
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
            </Fieldset>
            <Fieldset title="Responsável Financeiro">
              <DropDown
                v-model="enrollment.data.financeData.representative.discriminator"
                :errors="enrollment.errors.financeData.representative.discriminator"
                :size="2"
                :options="discriminators"
                :disabled="!!enrollment.data.sentAt || !underage"
                label="CPF ou CNPJ" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator == null"
                :size="3"
                disabled
                label="Documento" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativePerson'"
                v-model="enrollment.data.financeData.representative.cpf"
                :errors="enrollment.errors.financeData.representative.cpf"
                :size="3"
                :min-size="14"
                :disabled="!!enrollment.data.sentAt || !underage"
                cpf
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativeCompany'"
                v-model="enrollment.data.financeData.representative.cnpj"
                :errors="enrollment.errors.financeData.representative.cnpj"
                :size="3"
                :min-size="18"
                :disabled="!!enrollment.data.sentAt || !underage"
                cnpj
                label="CNPJ"
                mask="##.###.###/####-##"
                hint="Ex: 00.000.000/0000-00" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator == null"
                :size="4"
                disabled
                label="Nome" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativePerson'"
                v-model="enrollment.data.financeData.representative.name"
                :errors="enrollment.errors.financeData.representative.name"
                :size="4"
                :disabled="!!enrollment.data.sentAt || !underage"
                label="Nome completo" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativeCompany'"
                v-model="enrollment.data.financeData.representative.name"
                :errors="enrollment.errors.financeData.representative.name"
                :size="4"
                :disabled="!!enrollment.data.sentAt || !underage"
                label="Razão Social" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator == null"
                :size="3"
                disabled
                label="Contato" />
              <InputBox
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativeCompany'"
                v-model="enrollment.data.financeData.representative.contact"
                :errors="enrollment.errors.financeData.representative.contact"
                :size="3"
                :disabled="!!enrollment.data.sentAt || !underage"
                label="Pessoa de Contato" />
              <DropDown
                v-if="enrollment.data.financeData.representative.discriminator
                  == 'RepresentativePerson'"
                v-model="enrollment.data.financeData.representative.relationshipId"
                :errors="enrollment.errors.financeData.representative.relationshipId"
                :size="3"
                :options="enrollment.options.relationships"
                :disabled="!!enrollment.data.sentAt || !underage"
                label="Relacionamento com o aluno" />
            </Fieldset>
            <ContactBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :disabled="!!enrollment.data.sentAt || !underage" />
            <AddressBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :options="enrollment.options"
              :disabled="!!enrollment.data.sentAt || !underage" />
            <Fieldset
              v-if="guarantorsAmount > 0"
              title="Fiadores">
              <Multi
                v-model="enrollment.data.financeData.guarantors"
                :errors="enrollment.errors.financeData"
                :default="emptyGuarantor"
                :disabled="!!enrollment.data.sentAt"
                :max-amount="guarantorsAmount"
                error-key="guarantors">
                <template slot-scope="{ item, error }">
                  <InputBox
                    v-model="item.cpf"
                    :errors="error.cpf"
                    :size="4"
                    :min-size="14"
                    :disabled="!!enrollment.data.sentAt"
                    cpf
                    label="CPF"
                    mask="###.###.###-##"
                    hint="Ex: 000.000.000-00" />
                  <InputBox
                    v-model="item.name"
                    :errors="error.name"
                    :size="5"
                    :disabled="!!enrollment.data.sentAt"
                    label="Nome" />
                  <InputBox
                    v-model="item.relationshipId"
                    :errors="error.relationshipId"
                    :size="3"
                    :disabled="!!enrollment.data.sentAt"
                    label="Relacionamento" />
                  <AddressBlock
                    v-model="item"
                    :errors="error"
                    :disabled="!!enrollment.data.sentAt"
                    :options="enrollment.options" />
                  <ContactBlock
                    v-model="enrollment.data.financeData.representative"
                    :errors="enrollment.errors.financeData.representative"
                    :disabled="!!enrollment.data.sentAt" />
                  <Documents
                    v-model="item.documents"
                    :errors="error.documents"
                    :types="enrollment.options.guarantorDocuments"
                    :prefix="`onboarding/enrollment/${ id }/financeData/`"
                    :disabled="!!enrollment.data.sentAt"
                    :validations="validations" />
                </template>
              </Multi>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!!enrollment.data.sentAt"
                primary
                label="Próximo"
                @click="saveFinanceData" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="!enrollment.data.sentAt"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <Card
            closeable
            title="Enviar para Análise">
            <BaseErrors
              v-model="enrollment.messages.sendToApproval" />
            <p>Envie seus dados para a secetaria e para o
              departamento financeiro para aprovação.</p>
            <div class="center">
              <img
                :style="{ 'max-width': '8rem' }"
                src="../../assets/img/card.svg">
            </div>
            <div class="flex justify-end">
              <Btn
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
          :complete="!!enrollment.data.academicApproval"
          :warning="!enrollment.data.academicApproval && !!enrollment.data.reviewedAt"
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.academicApproval && !enrollment.data.reviewedAt"
            title="Aprovação da Secretaria">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval"
            title="Matrícula Aprovada pela Secretaria">
            A secretaria já aprovou sua matrícula.
          </Card>
          <Card
            v-if="!enrollment.data.academicApproval && !!enrollment.data.reviewedAt"
            title="Matrícula Reprovada pela Secretaria">
            A secretaria solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="step = 3" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="!!enrollment.data.financeApproval"
          :warning="!enrollment.data.financeApproval && !!enrollment.data.reviewedAt"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.financeApproval && !enrollment.data.reviewedAt"
            title="Aprovação do Financeiro">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="!!enrollment.data.financeApproval"
            title="Matrícula Aprovada pelo Financeiro">
            Nosso departamento financeiro já aprovou sua matrícula.
          </Card>
          <Card
            v-if="!enrollment.data.financeApproval && !!enrollment.data.reviewedAt"
            title="Matrícula Reprovada pelo Financeiro">
            Nosso departamento financeiro solicitou ajustes para completar sua matrícula.
            <div class="flex justify-end">
              <Btn
                primary
                label="Re-enviar"
                @click="step = 3" />
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
  import { debounce } from "lodash";
  import { parseDate, daysAgo } from "../../lib/helpers";

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
          relationship: "",
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
      underage() {
        return this.enrollment.underage;
      },
      daysRemaining() {
        return -daysAgo(parseDate(this.enrollment.data.deadline));
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
        const gradutionCurrentYear = new Date().getFullYear().toString() ===
          this.enrollment.data.personalData.highSchoolGraduationYear;

        return [
          !this.underage && "MinorAge",
          isNative && "Native",
          isForeigner && "Foreigner",
          hasDraft && "MilitaryDraft",
          isSpouse && "Spouse",
          foreignGraduation && "ForeignGraduation",
          gradutionCurrentYear && "GraduationYear",
        ].filter(a => a);
      },
    },
    watch: {
      "enrollment.data.personalData.zipcode": debounce(function findZipcode() {
        this.$store.dispatch("findZipcode");
      }, 500),
      "enrollment.data.personalData": {
        deep: true,
        handler() {
          this.$store.dispatch("copyResponsibleData");
        },
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
        if (this.enrollment.data.personalData.state === "valid") {
          this.step = 2;
        }
        if (this.enrollment.data.financeData.state === "valid") {
          this.step = 3;
        }
        if (this.enrollment.data.sentAt) {
          this.step = 3;
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
      },
      async saveFinanceData() {
        const token = this.id;
        const data = this.enrollment.data.financeData;
        await this.$store.dispatch("setFinanceData", { token, data });
        this.goToStep();
      },
      async submitEnrollment() {
        const token = this.id;
        await this.$store.dispatch("submitEnrollment", { token });
      },
    },
  };
</script>
