<template>
  <div>
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
                label="Nacionalidade"
                hint="Ex: Brasileiro" />
              <DropDown
                v-model="data.originCountryId"
                :size="3"
                :options="options.countries"
                label="País de Origem"
                hint="Ex: Brasil" />
              <DropDown
                v-model="data.birthStateId"
                :size="3"
                :options="options.states"
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
                required
                :options="options.states"
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
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_02"
                class="flex justify-between items-center">
                <div>Certidão de Nasciment ou Casamento</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_03"
                class="flex justify-between items-center">
                <div>Carteira de Identidade</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_04"
                class="flex justify-between items-center">
                <div>Título de Eleitor e Comprovante de Votação</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_05"
                class="flex justify-between items-center">
                <div>CPF</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_06"
                class="flex justify-between items-center">
                <div>Cartão de Vacinação (constando 3 doses de vacina contra
                  Hepatite B e vacina Dupla-adulto)</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div
                id="doc_07"
                class="flex justify-between items-center">
                <div>Documento Militar</div>
                <Btn
                  label="Enviar"
                  primary />
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
                v-model="data.responsibleDocumentType"
                :size="3"
                :options="options.documentType"
                label="CPF ou CNPJ" />
              <InputBox
                v-model="data.responsibleCPF"
                v-if="data.responsibleDocumentType == 'CPF'"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="data.responsibleCNPJ"
                v-if="data.responsibleDocumentType == 'CNPJ'"
                :size="3"
                label="CNPJ"
                mask="##.###.###/####-##"
                hint="Ex: 00.000.000/0000-00" />
              <InputBox
                v-model="data.responsibleName"
                :size="6"
                label="Razão Social" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.responsibleContact"
                :size="4"
                label="Contato" />
              <InputBox
                v-model="data.responsibleAddress"
                :size="8"
                label="Endereço Completo" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.responsiblePhone1"
                :size="4"
                label="Telefone"
                mask="(##) ####-####" />
              <InputBox
                v-model="data.responsiblePhone2"
                :size="4"
                label="Celular"
                mask="(##) #####-####" />
              <InputBox
                v-model="data.responsibleEmail"
                :size="4"
                label="E-mail" />
           </div>
          </Fieldset>
          <Fieldset title="Fiador">
            <div class="flex gutters flex-wrap">
              <DropDown
                v-model="data.guarantorDocumentType"
                :size="3"
                :options="options.documentType"
                label="CPF ou CNPJ" />
              <InputBox
                v-model="data.guarantorCPF"
                v-if="data.guarantorDocumentType == 'CPF'"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="data.guarantorCNPJ"
                v-if="data.guarantorDocumentType == 'CNPJ'"
                :size="3"
                label="CNPJ"
                mask="##.###.###/####-##"
                hint="Ex: 00.000.000/0000-00" />
              <InputBox
                v-model="data.guarantorName"
                :size="6"
                label="Razão Social" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.guarantorContact"
                :size="4"
                label="Contato" />
              <InputBox
                v-model="data.guarantorAddress"
                :size="8"
                label="Endereço Completo" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="data.guarantorPhone1"
                :size="4"
                label="Telefone"
                mask="(##) ####-####" />
              <InputBox
                v-model="data.guarantorPhone2"
                :size="4"
                label="Celular"
                mask="(##) #####-####" />
              <InputBox
                v-model="data.guarantorEmail"
                :size="4"
                label="E-mail" />
           </div>
          </Fieldset>
          <Fieldset title="Documentos do Fiador">
            <div class="p2 shadow0 rounded">
              <div class="flex justify-between items-center">
                <div>Carteira de Identidade</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div class="flex justify-between items-center">
                <div>CPF</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div class="flex justify-between items-center">
                <div>Comprovante de Endereço</div>
                <Btn
                  label="Enviar"
                  primary />
              </div>
              <hr>
              <div class="flex justify-between items-center">
                <div>Certidão de Nasciment ou Casamento</div>
                <Btn
                  label="Enviar"
                  primary />
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
  import axios from "axios";
  import { debounce, pickBy } from "lodash";

  export default {
    name: "Enrollment",
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

          responsibleDocumentType: null,
          responsibleCPF: "",
          responsibleCNPJ: "",
          responsibleName: "",
          responsibleContact: "",
          responsibleAddress: "",
          responsiblePhone1: "",
          responsiblePhone2: "",
          responsibleEmail: "",

          guarantorDocumentType: null,
          guarantorCPF: "",
          guarantorCNPJ: "",
          guarantorName: "",
          guarantorContact: "",
          guarantorAddress: "",
          guarantorPhone1: "",
          guarantorPhone2: "",
          guarantorEmail: "",
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
      };
    },
    watch: {
      data: {
        deep: true,
        handler: debounce(function save() { this.save(); }, 5000),
      },
    },
    mounted() {
      const uri = process.env.URL2;
      const token = this.$route.params.id;
      axios.get(`${uri}/api/Enrollments/${token}`).then((response) => {
        Object.assign(this.data, response.data.data);
        Object.assign(this.options, response.data.options);
        if (response.data.data.sendBy) {
          this.step = 2;
        }
      }, () => {
        this.$router.replace("/");
      });
    },
    methods: {
      save() {
        const uri = process.env.URL2;
        const token = this.$route.params.id;
        const data = pickBy(this.data, a => a);
        axios.patch(`${uri}/api/Enrollments/${token}`, data);
      },
    },
  };
</script>
