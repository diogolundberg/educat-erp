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
          :disabled="data.sendBy"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <Card
            title="Seus Dados"
            closeable
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
                  :size="6"
                  required
                  disabled
                  label="Nome"
                  hint="Seu nome completo" />
                <InputBox
                  v-model="data.personalData.assumedName"
                  :size="6"
                  label="Nome Social"
                  hint="Seu nome social" />
                <InputBox
                  v-model="data.personalData.cpf"
                  :size="3"
                  :min-size="14"
                  required
                  disabled
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <Date
                  v-model="data.personalData.birthDate"
                  :size="3"
                  :min-size="10"
                  required
                  label="Nascimento"
                  mask="##/##/####"
                  hint="Ex: 18/12/2001" />
                <DropDown
                  v-model="data.personalData.maritalStatusId"
                  :size="3"
                  :options="options.maritalStatuses"
                  required
                  label="Estado Civil" />
                <DropDown
                  v-model="data.personalData.genderId"
                  :size="3"
                  :options="options.genders"
                  required
                  label="Sexo" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.nationality"
                  :size="3"
                  required
                  label="Nacionalidade"
                  hint="Ex: Brasileiro" />
                <DropDown
                  v-model="data.personalData.birthCountryId"
                  :size="3"
                  :options="options.countries"
                  required
                  label="País de Origem"
                  hint="Ex: Brasil" />
                <DropDown
                  v-model="data.personalData.birthStateId"
                  :size="3"
                  :options="options.states"
                  required
                  label="UF de Nascimento" />
                <DropDown
                  v-model="data.personalData.birthCity"
                  :size="3"
                  :options="birthCities"
                  key-id="name"
                  required
                  label="Naturalidade"
                  hint="Cidade de Nascimento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.highSchoolGraduationYear"
                  :size="6"
                  :min-size="4"
                  :max-size="4"
                  mask="####"
                  required
                  label="Ano de conclusão do ensino médio"
                  hint="Ex: 2017" />
                <DropDown
                  v-model="data.personalData.highSchoolGraduationCountryId"
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
                  :size="6"
                  :min-size="6"
                  :max-size="50"
                  email
                  required
                  disabled
                  label="E-mail" />
                <InputBox
                  v-model="data.personalData.phoneNumber"
                  :size="6"
                  :min-size="13"
                  :max-size="14"
                  required
                  label="Telefone"
                  mask="(##) #########?"
                  hint="Ex: (31) 999999999" />
              </div>
            </Fieldset>
            <Fieldset title="Endereço">
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.zipcode"
                  :size="3"
                  :min-size="9"
                  required
                  label="CEP"
                  mask="#####-###"
                  hint="Ex: 30100-000" />
                <DropDown
                  v-model="data.personalData.addressKindId"
                  :size="3"
                  :options="options.addressKinds"
                  required
                  label="Tipo de Endereço" />
                <InputBox
                  v-model="data.personalData.streetAddress"
                  :size="6"
                  required
                  label="Logradouro"
                  hint="Sua rua, avenida, etc." />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.personalData.complementAddress"
                  :size="3"
                  required
                  label="Complemento" />
                <InputBox
                  v-model="data.personalData.neighborhood"
                  :size="3"
                  required
                  label="Bairro" />
                <DropDown
                  v-model="data.personalData.stateId"
                  :size="3"
                  :options="options.states"
                  required
                  label="Estado" />
                <InputBox
                  v-model="data.personalData.city"
                  :size="3"
                  required
                  label="Cidade" />
              </div>
            </Fieldset>
            <Fieldset title="Dados para o Censo">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.personalData.raceId"
                  :size="3"
                  :options="options.races"
                  required
                  label="Raça" />
                <DropDown
                  v-model="data.personalData.highSchoolKindId"
                  :size="3"
                  :options="options.highSchoolKinds"
                  required
                  label="Escola" />
                <InputBox
                  v-model="data.personalData.mothersName"
                  :size="6"
                  required
                  label="Nome completo da mãe" />
              </div>
            </Fieldset>
            <Fieldset title="Outras Informações">
              <div>
                <RadioGroup
                  v-model="data.personalData.handicap"
                  :options="options.handicap"
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
                  :options="options.disabilities" />
              </div>
            </Fieldset>
            <Fieldset title="Necessidades Especiais">
              <div class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="data.personalData.specialNeeds"
                  :options="options.specialNeeds" />
              </div>
            </Fieldset>
            <Fieldset title="Documentos">
              <div class="p2 shadow0 rounded">
                <template v-for="(doc, idx) in options.personalDocuments">
                  <div
                    :id="`doc_${doc.id}`"
                    :key="doc.id"
                    class="flex justify-between items-center">
                      <div>{{ doc.name }}</div>
                      <UploadButton
                        v-model="personalDocuments[doc.id]"
                        :prefix="data.externalId" />
                  </div>
                  <hr
                    v-if="idx < options.personalDocuments.length - 1"
                    :key="doc.id">
                </template>
              </div>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                @click="step = 2" />
            </div>
          </Card>
        </Step>
        <Step
          :disabled="data.sendBy"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <Card
            title="Dados Financeiros">
            <Fieldset title="Responsável Financeiro">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.responsible.discriminator"
                  :size="4"
                  :options="options.discriminators"
                  label="CPF ou CNPJ" />
                <InputBox
                  v-if="data.responsible.discriminator == null"
                  :size="4"
                  disabled
                  label="Documento" />
                <InputBox
                  v-model="data.responsible.cpf"
                  v-if="data.responsible.discriminator == 'Person'"
                  :size="4"
                  :min-size="14"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="data.responsible.cnpj"
                  v-if="data.responsible.discriminator == 'Company'"
                  :size="4"
                  :min-size="18"
                  cnpj
                  label="CNPJ"
                  mask="##.###.###/####-##"
                  hint="Ex: 00.000.000/0000-00" />
                <InputBox
                  v-if="data.responsible.discriminator == null"
                  :size="4"
                  disabled
                  label="Nome" />
                <InputBox
                  v-model="data.responsible.name"
                  v-if="data.responsible.discriminator == 'Person'"
                  :size="4"
                  label="Nome completo" />
                <InputBox
                  v-model="data.responsible.name"
                  v-if="data.responsible.discriminator == 'Company'"
                  :size="4"
                  label="Razão Social" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-if="data.responsible.discriminator == null"
                  :size="4"
                  disabled
                  label="Contato" />
                <InputBox
                  v-if="data.responsible.discriminator == 'Company'"
                  v-model="data.responsible.contact"
                  :size="4"
                  label="Pessoa de Contato" />
                <InputBox
                  v-if="data.responsible.discriminator == 'Person'"
                  v-model="data.responsible.relationship"
                  :size="4"
                  label="Relacionamento com o aluno" />
                <InputBox
                  v-model="data.responsible.streetAddress"
                  :size="4"
                  label="Logradouro" />
                <InputBox
                  v-model="data.responsible.complementAddress"
                  :size="4"
                  label="Complemento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.responsible.neighborhood"
                  :size="4"
                  label="Bairro" />
                <DropDown
                  v-model="data.responsible.stateId"
                  :size="4"
                  :options="options.states"
                  required
                  label="Estado" />
                <DropDown
                  v-model="data.responsible.city"
                  :size="4"
                  :options="options.cities"
                  key-id="name"
                  required
                  label="Cidade" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.responsible.landline"
                  :size="4"
                  label="Telefone"
                  mask="(##) ####-####" />
                <InputBox
                  v-model="data.responsible.phoneNumber"
                  :size="4"
                  label="Celular"
                  mask="(##) #####-####" />
                <InputBox
                  v-model="data.responsible.email"
                  :size="4"
                  label="E-mail" />
             </div>
            </Fieldset>
            <Fieldset title="Fiador">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="data.guarantor.discriminator"
                  :size="4"
                  :options="options.discriminators"
                  label="CPF ou CNPJ" />
                <InputBox
                  v-if="data.guarantor.discriminator == null"
                  :size="4"
                  disabled
                  label="Documento" />
                <InputBox
                  v-model="data.guarantor.cpf"
                  v-if="data.guarantor.discriminator == 'Person'"
                  :size="4"
                  :min-size="14"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="data.guarantor.cnpj"
                  v-if="data.guarantor.discriminator == 'Company'"
                  :size="4"
                  :min-size="18"
                  cnpj
                  label="CNPJ"
                  mask="##.###.###/####-##"
                  hint="Ex: 00.000.000/0000-00" />
                <InputBox
                  v-model="data.guarantor.name"
                  :size="4"
                  label="Razão Social" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.guarantor.contact"
                  :size="4"
                  label="Contato" />
                <InputBox
                  v-model="data.guarantor.streetAddress"
                  :size="4"
                  label="Endereço Completo" />
                <InputBox
                  v-model="data.guarantor.complementAddress"
                  :size="4"
                  label="Complemento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.guarantor.neighborhood"
                  :size="4"
                  label="Bairro" />
                <DropDown
                  v-model="data.guarantor.stateId"
                  :size="4"
                  :options="options.states"
                  required
                  label="Estado" />
                <DropDown
                  v-model="data.guarantor.cityId"
                  :size="4"
                  :options="options.cities"
                  key-id="name"
                  required
                  label="Cidade" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="data.guarantor.landline"
                  :size="4"
                  label="Telefone"
                  mask="(##) ####-####" />
                <InputBox
                  v-model="data.guarantor.phoneNumber"
                  :size="4"
                  label="Celular"
                  mask="(##) #####-####" />
                <InputBox
                  v-model="data.guarantor.email"
                  :size="4"
                  label="E-mail" />
             </div>
            </Fieldset>
            <Fieldset title="Documentos do Fiador">
              <div class="p2 shadow0 rounded">
                <div class="flex justify-between items-center">
                  <div>Carteira de Identidade</div>
                  <Btn />
                </div>
                <hr>
                <div class="flex justify-between items-center">
                  <div>CPF</div>
                  <Btn />
                </div>
                <hr>
                <div class="flex justify-between items-center">
                  <div>Comprovante de Endereço</div>
                  <Btn />
                </div>
                <hr>
                <div class="flex justify-between items-center">
                  <div>Certidão de Nasciment ou Casamento</div>
                  <Btn />
                </div>
              </div>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                @click="step = 3" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="!data.sendBy"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <Card title="Enviar para Análise">
            <p>Envie seus dados para a secetaria e para o departamento
              financeiro para aprovação.</p>
            <div class="flex justify-end">
              <Btn
                primary
                label="Enviar" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="data.sendBy"
          title="Aguardando Aprovação"
          description="A secretaria e o departamento financeiro estão analisando
            seus documentos">
          <Card title="Aguardando Aprovação">
            <p>A secretaria e o departamento financeiro estão analisando
              seus documentos.</p>
            <div class="flex justify-end">
              <Btn
                primary
                label="Enviar" />
            </div>
          </Card>
        </Step>
        <Step
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula." />
        <Step
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula." />
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
        data: {
          deadline: null,
          personalData: {
            realName: null,
            assumedName: null,
            birthDate: null,
            cpf: null,
            nationality: null,
            highSchoolGraduationYear: null,
            email: null,
            zipcode: null,
            streetAddress: null,
            complementAddress: null,
            neighborhood: null,
            phoneNumber: null,
            landline: null,
            mothersName: null,
            handicap: null,
            genderId: null,
            maritalStatusId: null,
            birthCity: null,
            birthStateId: null,
            birthCountryId: null,
            highSchoolGraduationCountryId: null,
            city: null,
            stateId: null,
            addressKindId: null,
            raceId: null,
            highSchoolKindId: null,
            specialNeeds: [],
            disabilities: [],
          },
          responsible: {
            discriminator: null,
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
          guarantor: {
            discriminator: null,
            cpf: "",
            cnpj: "",
            name: "",
            contact: "",
            streetAddress: "",
            complementAddress: "",
            neighborhood: "",
            phoneNumber: "",
            landline: "",
            email: "",
            cityId: null,
            stateId: null,
          },
          documents: {
            history: null,
            birthCertificate: null,
            rg: null,
            voterId: null,
            cpf: null,
            vaccination: null,
            military: null,
          },
        },
        options: {
          genders: [],
          maritalStatuses: [],
          countries: [],
          states: [],
          cities: [],
          addressKinds: [],
          nationalities: [],
          phoneType: [],
          races: [],
          highSchoolKinds: [],
          disabilities: [],
          specialNeeds: [],

          discriminators: [
            { id: "Person", name: "CPF" },
            { id: "Company", name: "CNPJ" },
          ],
          handicap: [
            { id: "yes", name: "Sim" },
            { id: "no", name: "Não" },
            { id: "unknown", name: "Não disponho da informação" },
          ],
        },
        step: 1,
        notifications: false,
        personalDocuments: {},
      };
    },
    computed: {
      daysRemaining() {
        const day = 1000 * 60 * 60 * 24;
        return Math.floor((new Date(this.data.deadline) - new Date()) / day);
      },
      birthCities() {
        const parentId = this.data.personalData.birthStateId;
        return this.options.cities.filter(a => a.stateId === parentId);
      },
    },
    watch: {
      "data.personalData": {
        deep: true,
        handler: debounce(function save() { this.save(); }, 1000),
      },
    },
    async mounted() {
      try {
        await this.$store.dispatch("getEnrollment", this.id);

        Object.assign(this.data, this.$store.getters.enrollment.data);
        Object.assign(this.options, this.$store.getters.enrollment.options);
        this.step = this.$store.getters.enrollment.sendBy ? 2 : 1;
      } catch (ex) {
        this.$router.push("/404");
      }
    },
    methods: {
      async save() {
        const token = this.id;
        const data = this.data.personalData;
        await this.$store.dispatch("setPersonalData", { token, data });
      },
    },
  };
</script>
