<template>
  <div>
    <Header @notifications="notifications = !notifications">
      <div class="flex items-center">
        <Icon name="clock" />
        <div class="mx2 px2 border-white-50 border-left inline-block h6">
          Olá, <strong>{{ data.name }}</strong><br>
          <template v-if="data.deadline">
            O seu processo de matrícula se encerra em {{ daysRemaining }} dias.
          </template>
        </div>
      </div>
    </Header>
    <Notifications
      v-if="notifications"
      @click="notifications = false" />

    <Stepper
      v-model="step"
      header
      class="max-width-4 m-auto">
      <Step
        :disabled="data.sendBy"
        title="Seus Dados"
        description="Preencha seus dados pessoais">
        <Card title="Seus Dados">
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
                v-model="data.name"
                :size="6"
                required
                label="Nome"
                hint="Seu nome completo" />
              <InputBox
                v-model="data.socialName"
                :size="6"
                required
                label="Nome Social"
                hint="Seu nome social" />
              <InputBox
                v-model="data.cpf"
                :size="3"
                :min-size="14"
                required
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <Date
                v-model="data.birthday"
                :size="3"
                :min-size="10"
                required
                label="Nascimento"
                mask="##/##/####"
                hint="Ex: 18/12/2001" />
              <DropDown
                v-model="data.civilStatusId"
                :size="3"
                :options="options.civilStatus"
                required
                label="Estado Civil" />
              <DropDown
                v-model="data.genderId"
                :size="3"
                :options="options.genders"
                required
                label="Sexo" />
            </div>
            <div class="flex gutters flex-wrap">
              <DropDown
                v-model="data.nationalityId"
                :size="3"
                :options="options.countries"
                required
                label="Nacionalidade"
                hint="Ex: Brasileiro" />
              <DropDown
                v-model="data.originCountryId"
                :size="3"
                :options="options.countries"
                required
                label="País de Origem"
                hint="Ex: Brasil" />
              <DropDown
                v-model="data.birthStateId"
                :size="3"
                :options="options.states"
                required
                label="UF de Nascimento" />
              <InputBox
                v-model="data.birthCity"
                :size="3"
                required
                label="Naturalidade"
                hint="Cidade de Nascimento" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.yearofHighSchoolGraduation"
                :size="6"
                :min-size="4"
                :max-size="4"
                mask="####"
                required
                label="Ano de conclusão do ensino médio"
                hint="Ex: 2017" />
              <DropDown
                v-model="data.countryOfGraduationFromHighSchoolId"
                :size="6"
                :options="options.countries"
                required
                label="País de conclusão do ensino médio" />
            </div>
          </Fieldset>
          <Fieldset title="Dados de Contato">
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.email"
                :size="6"
                :min-size="6"
                :max-size="50"
                email
                required
                label="E-mail" />
              <InputBox
                v-model="data.phoneNumber"
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
                v-model="data.cep"
                :size="3"
                :min-size="9"
                required
                label="CEP"
                mask="#####-###"
                hint="Ex: 30100-000" />
              <DropDown
                v-model="data.addressTypeId"
                :size="3"
                :options="options.addressTypes"
                required
                label="Tipo de Endereço" />
              <InputBox
                v-model="data.address"
                :size="6"
                required
                label="Logradouro"
                hint="Sua rua, avenida, etc." />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.number"
                :size="3"
                required
                label="Número" />
              <InputBox
                v-model="data.neighborhood"
                :size="3"
                required
                label="Bairro" />
              <InputBox
                v-model="data.city"
                :size="3"
                required
                label="Cidade" />
              <DropDown
                v-model="data.countryStateId"
                :size="3"
                :options="options.states"
                required
                label="Estado" />
            </div>
          </Fieldset>
          <Fieldset title="Dados para o Censo">
            <div class="flex gutters flex-wrap">
              <DropDown
                v-model="data.raceId"
                :size="3"
                :options="options.races"
                required
                label="Raça" />
              <DropDown
                v-model="data.schoolId"
                :size="3"
                :options="options.schools"
                required
                label="Escola" />
              <InputBox
                v-model="data.mothersName"
                :size="6"
                required
                label="Nome completo da mãe" />
            </div>
            <div>
              <RadioGroup
                v-model="data.handicaps"
                :options="options.handicaps"
                required
                label="Possui alguma Deficiência, Transtorno Global do
                  Desenvolvimento, ou Habilidades/Superdotação?" />
            </div>
            <div
              v-if="data.handicaps == 'yes'"
              class="flex gutters flex-wrap">
              <h4>Selecione:</h4>
              <CheckGroup
                v-model="data.disabilities"
                :options="options.disabilities" />
            </div>
          </Fieldset>
          <Fieldset title="Documentos">
            <div class="p2 shadow0 rounded">
              <div
                id="doc_01"
                class="flex justify-between items-center">
                <div>Histórico Escolar do Ensino Médio</div>
                <UploadButton
                  v-model="data.documents.history"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_02"
                class="flex justify-between items-center">
                <div>Certidão de Nasciment ou Casamento</div>
                <UploadButton
                  v-model="data.documents.birthCertificate"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_03"
                class="flex justify-between items-center">
                <div>Carteira de Identidade</div>
                <UploadButton
                  v-model="data.documents.rg"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_04"
                class="flex justify-between items-center">
                <div>Título de Eleitor e Comprovante de Votação</div>
                <UploadButton
                  v-model="data.documents.voterId"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_05"
                class="flex justify-between items-center">
                <div>CPF</div>
                <UploadButton
                  v-model="data.documents.cpf"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_06"
                class="flex justify-between items-center">
                <div>Cartão de Vacinação (constando 3 doses de vacina contra
                  Hepatite B e vacina Dupla-adulto)</div>
                <UploadButton
                  v-model="data.documents.vaccination"
                  :prefix="data.externalId" />
              </div>
              <hr>
              <div
                id="doc_07"
                class="flex justify-between items-center">
                <div>Documento Militar</div>
                <UploadButton
                  v-model="data.documents.military"
                  :prefix="data.externalId" />
              </div>
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
        v-if="false"
        :disabled="data.sendBy"
        title="Dados Financeiros"
        description="Aqui você insere seus dados de pagamento.">
        <Card
          title="Dados Financeiros">
          <Fieldset title="Responsável Financeiro">
            <div class="flex gutters flex-wrap">
              <DropDown
                v-model="data.responsible.documenttype"
                :size="3"
                :options="options.documentType"
                label="CPF ou CNPJ" />
              <InputBox
                v-model="data.responsible.cpf"
                v-if="data.responsible.documenttype == 'CPF'"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="data.responsible.cnpj"
                v-if="data.responsible.documenttype == 'CNPJ'"
                :size="3"
                label="CNPJ"
                mask="##.###.###/####-##"
                hint="Ex: 00.000.000/0000-00" />
              <InputBox
                v-model="data.responsible.name"
                :size="6"
                label="Razão Social" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.responsible.contact"
                :size="4"
                label="Contato" />
              <InputBox
                v-model="data.responsible.address"
                :size="8"
                label="Endereço Completo" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.responsible.phone1"
                :size="4"
                label="Telefone"
                mask="(##) ####-####" />
              <InputBox
                v-model="data.responsible.phone2"
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
                v-model="data.guarantor.documenttype"
                :size="3"
                :options="options.documentType"
                label="CPF ou CNPJ" />
              <InputBox
                v-model="data.guarantor.cpf"
                v-if="data.guarantor.documenttype == 'CPF'"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="data.guarantor.cnpj"
                v-if="data.guarantor.documenttype == 'CNPJ'"
                :size="3"
                label="CNPJ"
                mask="##.###.###/####-##"
                hint="Ex: 00.000.000/0000-00" />
              <InputBox
                v-model="data.guarantor.name"
                :size="6"
                label="Razão Social" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.guarantor.contact"
                :size="4"
                label="Contato" />
              <InputBox
                v-model="data.guarantor.address"
                :size="8"
                label="Endereço Completo" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.guarantor.phone1"
                :size="4"
                label="Telefone"
                mask="(##) ####-####" />
              <InputBox
                v-model="data.guarantor.phone2"
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
          <p>Envie seus dados para a secetaria e para o departamento financeiro
            para aprovação.</p>
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
          name: null,
          socialName: null,
          cpf: null,
          birthday: null,
          civilStatusId: null,
          genderId: null,
          nationalityId: null,
          originCountryId: null,
          birthStateId: null,
          birthCity: null,
          yearofHighSchoolGraduation: null,
          countryOfGraduationFromHighSchoolId: null,
          email: null,
          phoneTypeId: null,
          phoneNumber: null,
          cep: null,
          address: null,
          number: null,
          neighborhood: null,
          city: null,
          countryStateId: null,
          addressTypeId: null,
          raceId: null,
          schoolId: null,
          mothersName: null,
          hasHandicaps: true,
          disabilities: [],
          responsible: {
            documenttype: null,
            cpf: "",
            cnpj: "",
            name: "",
            contact: "",
            address: "",
            phone1: "",
            phone2: "",
            email: "",
          },
          guarantor: {
            documenttype: null,
            cpf: "",
            cnpj: "",
            name: "",
            contact: "",
            address: "",
            phone1: "",
            phone2: "",
            email: "",
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
          civilStatus: [],
          countries: [],
          states: [],
          addressTypes: [],
          nationalities: [],
          phoneType: [],
          races: [],
          schools: [],
          disabilities: [],

          documentType: [
            { id: "CPF", name: "CPF" },
            { id: "CNPJ", name: "CNPJ" },
          ],
          handicaps: [
            { id: "yes", name: "Sim" },
            { id: "no", name: "Não" },
            { id: "unknown", name: "Não disponho da informação" },
          ],
        },
        step: 1,
        notifications: false,
      };
    },
    computed: {
      daysRemaining() {
        const day = 1000 * 60 * 60 * 24;
        return Math.floor((new Date(this.data.deadline) - new Date()) / day);
      },
    },
    watch: {
      data: {
        deep: true,
        handler: debounce(function save() { this.save(); }, 1000),
      },
    },
    async mounted() {
      await this.$store.dispatch("getEnrollment", this.id);

      Object.assign(this.data, this.$store.getters.enrollment.data);
      Object.assign(this.options, this.$store.getters.enrollment.options);
      this.step = this.$store.getters.enrollment.sendBy ? 2 : 1;
    },
    methods: {
      async save() {
        const token = this.id;
        const { data } = this;
        await this.$store.dispatch("setEnrollment", { token, data });
      },
    },
  };
</script>
