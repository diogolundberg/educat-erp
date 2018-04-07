<template>
  <div>
    <Header @notifications="notifications = !notifications">
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
            title="Seus Dados"
            closeable
            :complete="enrollment.data.personalData.state === 'valid'"
            :error="enrollment.data.personalData.state === 'invalid'"
            @close="step = 0">
            <div
              slot="title"
              class="center mb4n mt2">
              <img
                src="../../assets/img/people.svg"
                class="rounded border4 border shadow2 x6 y6 bg-white">
            </div>
            <Fieldset>
              <div class="flex gutters flex-wrap">
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
              </div>
              <div class="flex gutters flex-wrap">
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
              </div>
              <div class="flex gutters flex-wrap">
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
              </div>
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
              <div class="flex gutters flex-wrap">
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
              </div>
            </Fieldset>
            <Fieldset title="Outras Informações">
              <div>
                <RadioGroup
                  v-model="enrollment.data.personalData.handicap"
                  :errors="enrollment.errors.personalData.handicap"
                  :options="handicap"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Possui alguma Deficiência, Transtorno Global do
                    Desenvolvimento, ou Habilidades/Superdotação?" />
              </div>
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <div
                class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="enrollment.data.personalData.disabilities"
                  :errors="enrollment.errors.personalData.disabilities"
                  :options="enrollment.options.disabilities"
                  :disabled="!!enrollment.data.sentAt" />
              </div>
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <div class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="enrollment.data.personalData.specialNeeds"
                  :errors="enrollment.errors.personalData.specialNeeds"
                  :options="enrollment.options.specialNeeds"
                  :disabled="!!enrollment.data.sentAt"
                  :filter="enrollment.data.personalData.disabilities"
                  filter-key="disabilityId" />
              </div>
            </Fieldset>
            <Fieldset title="Documentos">
              <Documents
                v-model="enrollment.data.personalData.documents"
                :types="enrollment.options.personalDocuments"
                :prefix="`onboarding/enrollment/${ id }/personalData/`"
                :disabled="!!enrollment.data.sentAt" />
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
            <Fieldset title="Dados Financeiros">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.financeData.paymentMethodId"
                  :errors="enrollment.errors.financeData.paymentMethodId"
                  :size="6"
                  :options="enrollment.options.paymentMethod"
                  :disabled="!!enrollment.data.sentAt"
                  label="Meio de Pagamento" />
                <DropDown
                  v-model="enrollment.data.financeData.planId"
                  :errors="enrollment.errors.financeData.planId"
                  :size="6"
                  :options="enrollment.options.plans"
                  :disabled="!!enrollment.data.sentAt"
                  label="Meio de Pagamento" />
              </div>
            </Fieldset>
            <Fieldset title="Responsável Financeiro">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.financeData.representative.discriminator"
                  :errors="enrollment.errors.financeData.representative.discriminator"
                  :size="2"
                  :options="discriminators"
                  :disabled="!!enrollment.data.sentAt"
                  label="CPF ou CNPJ" />
                <InputBox
                  v-if="enrollment.data.financeData.representative.discriminator == null"
                  :size="3"
                  disabled
                  label="Documento" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.cpf"
                  :errors="enrollment.errors.financeData.representative.cpf"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="3"
                  :min-size="14"
                  :disabled="!!enrollment.data.sentAt"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.cnpj"
                  :errors="enrollment.errors.financeData.representative.cnpj"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="3"
                  :min-size="18"
                  :disabled="!!enrollment.data.sentAt"
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
                  v-model="enrollment.data.financeData.representative.name"
                  :errors="enrollment.errors.financeData.representative.name"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Nome completo" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.name"
                  :errors="enrollment.errors.financeData.representative.name"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
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
                  :disabled="!!enrollment.data.sentAt"
                  label="Pessoa de Contato" />
                <DropDown
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  v-model="enrollment.data.financeData.representative.relationshipId"
                  :errors="enrollment.errors.financeData.representative.relationshipId"
                  :size="3"
                  :options="enrollment.options.relationships"
                  :disabled="!!enrollment.data.sentAt"
                  label="Relacionamento com o aluno" />
              </div>
            </Fieldset>
            <ContactBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :disabled="!!enrollment.data.sentAt" />
            <AddressBlock
              v-model="enrollment.data.financeData.representative"
              :errors="enrollment.errors.financeData.representative"
              :options="enrollment.options"
              :disabled="!!enrollment.data.sentAt" />
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
                  <div class="flex gutters flex-wrap">
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
                  </div>
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
                    :disabled="!!enrollment.data.sentAt" />
                </template>
              </Multi>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                :disabled="!!enrollment.data.sentAt"
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
            <BaseErrors v-model="enrollment.messages" />
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
              success
              v-model="enrollment.messages" />
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
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.academicApproval"
            title="Aprovação da Secretaria">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval"
            title="Matrícula Aprovada pela Secretaria">
            A secretaria já aprovou sua matrícula.
          </Card>
        </Step>
        <Step
          :complete="!!enrollment.data.financeApproval"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.academicApproval"
            title="Aprovação do Financeiro">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval"
            title="Matrícula Aprovada pelo Financeiro">
            Nosso departamento financeiro já aprovou sua matrícula.
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
      daysRemaining() {
        const day = 1000 * 60 * 60 * 24;
        const remaining = new Date(this.enrollment.data.deadline) - new Date();
        return Math.floor(remaining / day);
      },
      guarantorsAmount() {
        const { planId } = this.enrollment.data.financeData;
        const plan = this.enrollment.options.plans.find(a => a.id === planId);
        return plan && plan.guarantors;
      },
    },
    watch: {
      "enrollment.data.personalData.zipcode": debounce(function findZipcode() {
        this.$store.dispatch("findZipcode");
      }, 500),
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
