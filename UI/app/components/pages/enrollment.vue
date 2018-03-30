<template>
  <div>
    <Header @notifications="notifications = !notifications">
      <div class="flex items-center">
        <Icon name="clock" />
        <div class="mx2 px2 border-white-50 border-left inline-block h6">
          Olá, <strong>{{ data.personalData.realName }}</strong><br>
          <template v-if="data.deadline">
            O seu processo de matrícula se encerra em {{ daysRemaining }} dias.
          </template>
        </div>
      </div>
    </Header>
    <Notifications
      v-if="notifications"
      @click="notifications = false" />

    <Spinner :active="!data.deadline">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          :disabled="!!data.sendDate"
          :complete="data.personalData.state === 'valid'"
          :error="data.personalData.state === 'invalid'"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <Card
            title="Seus Dados"
            closeable
            :complete="data.personalData.state === 'valid'"
            :error="data.personalData.state === 'invalid'"
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
                  v-model="data.personalData.realName"
                  :errors="errors.personalData.RealName"
                  :size="6"
                  required
                  disabled
                  label="Nome"
                  hint="Seu nome completo" />
                <InputBox
                  v-model="data.personalData.assumedName"
                  :errors="errors.personalData.AssumedName"
                  :size="6"
                  label="Nome Social"
                  hint="Seu nome social" />
                <InputBox
                  v-model="data.personalData.cpf"
                  :errors="errors.personalData.Cpf"
                  :size="3"
                  :min-size="14"
                  required
                  disabled
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <Date
                  v-model="data.personalData.birthDate"
                  :errors="errors.personalData.BirthDate"
                  :size="3"
                  :min-size="10"
                  required
                  label="Nascimento"
                  mask="##/##/####"
                  hint="Ex: 18/12/2001" />
                <DropDown
                  v-model="data.personalData.maritalStatusId"
                  :errors="errors.personalData.MaritalStatusId"
                  :size="3"
                  :options="options.maritalStatuses"
                  required
                  label="Estado Civil" />
                <DropDown
                  v-model="data.personalData.genderId"
                  :errors="errors.personalData.GenderId"
                  :size="3"
                  :options="options.genders"
                  required
                  label="Sexo" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.nationality"
                  :errors="errors.personalData.Nationality"
                  :size="3"
                  required
                  label="Nacionalidade"
                  hint="Ex: Brasileiro" />
                <DropDown
                  v-model="data.personalData.birthCountryId"
                  :errors="errors.personalData.BirthCountryId"
                  :size="3"
                  :options="options.countries"
                  required
                  label="País de Origem"
                  hint="Ex: Brasil" />
                <DropDown
                  v-model="data.personalData.birthStateId"
                  :errors="errors.personalData.BirthStateId"
                  :size="3"
                  :options="options.states"
                  required
                  label="UF de Nascimento" />
                <DropDown
                  v-model="data.personalData.birthCityId"
                  :errors="errors.personalData.BirthCityId"
                  :size="3"
                  :options="options.cities"
                  :filter="data.personalData.birthStateId"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Naturalidade"
                  hint="Cidade de Nascimento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.highSchoolGraduationYear"
                  :errors="errors.personalData.HighSchoolGraduationYear"
                  :size="6"
                  :min-size="4"
                  :max-size="4"
                  mask="####"
                  required
                  label="Ano de conclusão do ensino médio"
                  hint="Ex: 2017" />
                <DropDown
                  v-model="data.personalData.highSchoolGraduationCountryId"
                  :errors="errors.personalData.HighSchoolGraduationCountryId"
                  :size="6"
                  :options="options.countries"
                  required
                  label="País de conclusão do ensino médio" />
              </div>
            </Fieldset>
            <Fieldset title="Dados de Contato">
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.email"
                  :errors="errors.personalData.Email"
                  :size="4"
                  :min-size="6"
                  :max-size="50"
                  email
                  required
                  disabled
                  label="E-mail" />
                <InputBox
                  v-model="data.personalData.phoneNumber"
                  :errors="errors.personalData.PhoneNumber"
                  :size="4"
                  :min-size="13"
                  :max-size="14"
                  required
                  label="Telefone"
                  mask="(##) #########?"
                  hint="Ex: (31) 999999999" />
                <InputBox
                  v-model="data.personalData.landline"
                  :errors="errors.personalData.landline"
                  :size="4"
                  :min-size="13"
                  :max-size="14"
                  required
                  label="Telefone Fixo"
                  mask="(##) #########?"
                  hint="Ex: (31) 322222222" />
              </div>
            </Fieldset>
            <Fieldset title="Endereço">
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.zipcode"
                  :errors="errors.personalData.Zipcode"
                  :size="3"
                  :min-size="9"
                  required
                  label="CEP"
                  mask="#####-###"
                  hint="Ex: 30100-000" />
                <DropDown
                  v-model="data.personalData.addressKindId"
                  :errors="errors.personalData.AddressKindId"
                  :size="3"
                  :options="options.addressKinds"
                  required
                  label="Tipo de Endereço" />
                <InputBox
                  v-model="data.personalData.streetAddress"
                  :errors="errors.personalData.StreetAddress"
                  :size="6"
                  required
                  label="Logradouro"
                  hint="Sua rua, avenida, etc." />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.complementAddress"
                  :errors="errors.personalData.ComplementAddress"
                  :size="3"
                  required
                  label="Complemento" />
                <InputBox
                  v-model="data.personalData.neighborhood"
                  :errors="errors.personalData.Neighborhood"
                  :size="3"
                  required
                  label="Bairro" />
                <DropDown
                  v-model="data.personalData.stateId"
                  :errors="errors.personalData.StateId"
                  :size="3"
                  :options="options.states"
                  required
                  label="Estado" />
                <DropDown
                  v-model="data.personalData.cityId"
                  :errors="errors.personalData.cityId"
                  :size="3"
                  :options="options.cities"
                  :filter="data.personalData.stateId"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Cidade"
                  hint="Cidade onde Mora" />
              </div>
            </Fieldset>
            <Fieldset title="Dados para o Censo">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.personalData.raceId"
                  :errors="errors.personalData.RaceId"
                  :size="3"
                  :options="options.races"
                  required
                  label="Raça" />
                <DropDown
                  v-model="data.personalData.highSchoolKindId"
                  :errors="errors.personalData.HighSchoolKindId"
                  :size="3"
                  :options="options.highSchoolKinds"
                  required
                  label="Escola" />
                <InputBox
                  v-model="data.personalData.mothersName"
                  :errors="errors.personalData.MothersName"
                  :size="6"
                  required
                  label="Nome completo da mãe" />
              </div>
            </Fieldset>
            <Fieldset title="Outras Informações">
              <div>
                <RadioGroup
                  v-model="data.personalData.handicap"
                  :errors="errors.personalData.Handicap"
                  :options="handicap"
                  required
                  label="Possui alguma Deficiência, Transtorno Global do
                    Desenvolvimento, ou Habilidades/Superdotação?" />
              </div>
              <div
                v-if="data.personalData.handicap == 'yes'"
                class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="data.personalData.disabilities"
                  :errors="errors.personalData.Disabilities"
                  :options="options.disabilities" />
              </div>
            </Fieldset>
            <Fieldset title="Necessidades Especiais">
              <div class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="data.personalData.specialNeeds"
                  :errors="errors.personalData.SpecialNeeds"
                  :options="options.specialNeeds" />
              </div>
            </Fieldset>
            <Fieldset title="Documentos">
              <Documents
                v-model="data.personalData.documents"
                :types="options.personalDocuments"
                :prefix="`onboarding/enrollment/${ id }/personalData/`" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                @click="savePersonalData" />
            </div>
          </Card>
        </Step>
        <Step
          :disabled="!!data.sendDate"
          :complete="data.financeData.state == 'valid'"
          :error="data.financeData.state == 'invalid'"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <Card
            :complete="data.financeData.state === 'valid'"
            :error="data.financeData.state === 'invalid'"
            title="Dados Financeiros">
            <Fieldset title="Dados Financeiros">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.financeData.paymentMethodId"
                  :errors="errors.financeData.paymentMethodId"
                  :size="6"
                  :options="options.paymentMethod"
                  label="Meio de Pagamento" />
                <DropDown
                  v-model="data.financeData.planId"
                  :errors="errors.financeData.planId"
                  :size="6"
                  :options="options.plans"
                  label="Meio de Pagamento" />
              </div>
            </Fieldset>
            <Fieldset title="Responsável Financeiro">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.financeData.representative.discriminator"
                  :errors="errors.financeData.representative.discriminator"
                  :size="4"
                  :options="discriminators"
                  label="CPF ou CNPJ" />
                <InputBox
                  v-if="data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Documento" />
                <InputBox
                  v-model="data.financeData.representative.cpf"
                  :errors="errors.financeData.representative.cpf"
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="4"
                  :min-size="14"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="data.financeData.representative.cnpj"
                  :errors="errors.financeData.representative.cnpj"
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="4"
                  :min-size="18"
                  cnpj
                  label="CNPJ"
                  mask="##.###.###/####-##"
                  hint="Ex: 00.000.000/0000-00" />
                <InputBox
                  v-if="data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Nome" />
                <InputBox
                  v-model="data.financeData.representative.name"
                  :errors="errors.financeData.representative.name"
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="4"
                  label="Nome completo" />
                <InputBox
                  v-model="data.financeData.representative.name"
                  :errors="errors.financeData.representative.name"
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="4"
                  label="Razão Social" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-if="data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Contato" />
                <InputBox
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  v-model="data.financeData.representative.contact"
                  :errors="errors.financeData.representative.contact"
                  :size="4"
                  label="Pessoa de Contato" />
                <InputBox
                  v-if="data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  v-model="data.financeData.representative.relationship"
                  :errors="errors.financeData.representative.relationship"
                  :size="4"
                  label="Relacionamento com o aluno" />
                <InputBox
                  v-model="data.financeData.representative.streetAddress"
                  :errors="errors.financeData.representative.streetAddress"
                  :size="4"
                  label="Logradouro" />
                <InputBox
                  v-model="data.financeData.representative.complementAddress"
                  :errors="errors.financeData.representative.complementAddress"
                  :size="4"
                  label="Complemento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.financeData.representative.neighborhood"
                  :errors="errors.financeData.representative.neighborhood"
                  :size="4"
                  label="Bairro" />
                <DropDown
                  v-model="data.financeData.representative.stateId"
                  :errors="errors.financeData.representative.stateId"
                  :size="4"
                  :options="options.states"
                  required
                  label="Estado" />
                <DropDown
                  v-model="data.financeData.representative.cityId"
                  :errors="errors.financeData.representative.cityId"
                  :size="4"
                  :options="options.cities"
                  :filter="data.financeData.representative.stateId"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Cidade" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.financeData.representative.landline"
                  :errors="errors.financeData.representative.landline"
                  :size="4"
                  label="Telefone"
                  mask="(##) ####-####" />
                <InputBox
                  v-model="data.financeData.representative.phoneNumber"
                  :errors="errors.financeData.representative.phoneNumber"
                  :size="4"
                  label="Celular"
                  mask="(##) #####-####" />
                <InputBox
                  v-model="data.financeData.representative.email"
                  :errors="errors.financeData.representative.email"
                  :size="4"
                  label="E-mail" />
             </div>
            </Fieldset>
            <Fieldset
              title="Fiadores">
              <Multi
                v-model="data.financeData.guarantors"
                :errors="errors.financeData"
                :default="emptyGuarantor"
                error-key="guarantors">
                <template slot-scope="{ item, error }">
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.cpf"
                      :errors="error.cpf"
                      :size="4"
                      :min-size="14"
                      cpf
                      label="CPF"
                      mask="###.###.###-##"
                      hint="Ex: 000.000.000-00" />
                    <InputBox
                      v-model="item.name"
                      :errors="error.name"
                      :size="8"
                      label="Nome" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.relationship"
                      :errors="error.relationship"
                      :size="4"
                      label="Relacionamento" />
                    <InputBox
                      v-model="item.streetAddress"
                      :errors="error.streetAddress"
                      :size="4"
                      label="Endereço Completo" />
                    <InputBox
                      v-model="item.complementAddress"
                      :errors="error.complementAddress"
                      :size="4"
                      label="Complemento" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.neighborhood"
                      :errors="error.neighborhood"
                      :size="4"
                      label="Bairro" />
                    <DropDown
                      v-model="item.stateId"
                      :errors="error.stateId"
                      :size="4"
                      :options="options.states"
                      required
                      label="Estado" />
                    <DropDown
                      v-model="item.cityId"
                      :errors="error.cityId"
                      :size="4"
                      :options="options.cities"
                      :filter="item.stateId"
                      filter-key="stateId"
                      key-id="name"
                      required
                      label="Cidade"
                      hint="Cidade onde Mora" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.landline"
                      :errors="error.landline"
                      :size="4"
                      label="Telefone"
                      mask="(##) ####-####" />
                    <InputBox
                      v-model="item.phoneNumber"
                      :errors="error.phoneNumber"
                      :size="4"
                      label="Celular"
                      mask="(##) #####-####" />
                    <InputBox
                      v-model="item.email"
                      :errors="error.email"
                      :size="4"
                      label="E-mail" />
                  </div>
                  <Documents
                    v-model="item.documents"
                    :errors="error.documents"
                    :types="options.guarantorDocuments"
                    :prefix="`onboarding/enrollment/${ id }/financeData/`" />
                </template>
              </Multi>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                @click="saveFinanceData" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="!data.sendDate"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <Card
            closeable
            title="Enviar para Análise">
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
          v-if="data.sendDate"
          complete
          title="Aguardando Aprovação"
          description="A secretaria e o departamento financeiro estão analisando
            seus documentos">
          <Card title="Dados enviados">
            <p>Seus dados foram enviados. Agora a secretaria e o departamento
              financeiro estão analisando seus documentos.</p>
            <div class="center">
              <Animation
                name="success" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="data.academicApproval"
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <Card
            v-if="!data.academicApproval"
            title="Aprovação da Secretaria">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="data.academicApproval"
            title="Matrícula Aprovada pela Secretaria">
            A secretaria já aprovou sua matrícula.
          </Card>
        </Step>
        <Step
          :complete="data.financeApproval"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <Card
            v-if="!data.academicApproval"
            title="Aprovação do Financeiro">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="data.academicApproval"
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
      data() {
        return this.$store.getters.enrollment.data;
      },
      options() {
        return this.$store.getters.enrollment.options;
      },
      errors() {
        return this.$store.getters.enrollment.errors;
      },
      daysRemaining() {
        const day = 1000 * 60 * 60 * 24;
        return Math.floor((new Date(this.data.deadline) - new Date()) / day);
      },
    },
    async mounted() {
      try {
        await this.$store.dispatch("getEnrollment", this.id);
        this.step = this.$store.getters.enrollment.sendDate ? 3 : 1;
      } catch (ex) {
        this.$router.push("/404");
      }
    },
    methods: {
      async savePersonalData() {
        const token = this.id;
        const data = this.data.personalData;
        await this.$store.dispatch("setPersonalData", { token, data });
        if (this.data.personalData.state === "valid") {
          this.step = 2;
        }
      },
      async saveFinanceData() {
        const token = this.id;
        const data = this.data.financeData;
        await this.$store.dispatch("setFinanceData", { token, data });
        if (this.data.financeData.state === "valid") {
          this.step = 3;
        }
      },
      async submitEnrollment() {
        const token = this.id;
        await this.$store.dispatch("submitEnrollment", { token });
      },
    },
  };
</script>
