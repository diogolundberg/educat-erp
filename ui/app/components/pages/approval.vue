<template>
  <div>
    <DataForm
      :id="id"
      :title="$route.meta.title"
      :endpoint="endpoint"
      :editable="false"
      class="max-width-4 m-auto">
      <div slot-scope="{ item, errors }">
        <template v-if="item.data">
          <Fieldset title="Dados Gerais">
            <div class="col-12 center mb2">
              <img
                v-if="item.data.photo"
                :src="item.data.photo"
                class="rounded shadow2 x6 y6 bg-white">
              <img
                v-if="!item.data.photo"
                src="../../assets/img/people.svg"
                class="rounded shadow2 x6 y6 bg-white">
            </div>
            <KeyValue
              :value="item.data.name"
              title="Nome" />
            <KeyValue
              :value="item.data.assumedName"
              title="Nome Social" />
            <KeyValue
              :value="item.data.cpf"
              title="CPF" />
            <template v-if="type=='academic'">
              <KeyValue
                :value="item.data.birthDate"
                title="Nascimento" />
              <KeyValue
                :value="item.data.maritalStatus"
                title="Estado Civil" />
              <KeyValue
                :value="item.data.gender"
                title="Sexo" />
              <KeyValue
                :value="item.data.nationality"
                title="Nacionalidade" />
              <KeyValue
                :value="item.data.birthCountry"
                title="País de Origem" />
              <KeyValue
                :value="item.data.birthCity"
                title="Naturalidade" />
              <KeyValue
                :value="item.data.birthState"
                title="Estado de Nascimento" />
              <KeyValue
                :value="item.data.highSchoolGraduationYear"
                title="Graduou em" />
              <KeyValue
                :value="item.data.highSchoolGraduationCountry"
                title="País da Escola" />
            </template>
          </Fieldset>
          <Fieldset title="Contato">
            <KeyValue
              :value="item.data.email"
              title="E-Mail" />
            <KeyValue
              :value="item.data.phoneNumber"
              title="Telefone" />
            <KeyValue
              :value="item.data.landline"
              title="Telefone Fixo" />
          </Fieldset>
          <Fieldset
            v-if="type=='academic'"
            title="Endereço">
            <KeyValue
              :value="item.data.zipcode"
              title="CEP" />
            <KeyValue
              :value="item.data.addressKind"
              title="Tipo de Endereço" />
            <KeyValue
              :value="item.data.streetAddress"
              title="Logradouro" />
            <KeyValue
              :value="item.data.addressNumber"
              title="Número" />
            <KeyValue
              :value="item.data.complementAddress"
              title="Complemento" />
            <KeyValue
              :value="item.data.neighborhood"
              title="Bairro" />
            <KeyValue
              :value="item.data.city"
              title="Cidade" />
            <KeyValue
              :value="item.data.state"
              title="Estado" />
          </Fieldset>
          <Fieldset
            v-if="type=='academic'"
            title="Dados para o Censo">
            <KeyValue
              :value="item.data.mothersName"
              title="Nome da Mãe" />
            <KeyValue
              :value="item.data.race"
              title="Raça" />
            <KeyValue
              :value="item.data.highSchollKind"
              title="Tipo da Escola" />
          </Fieldset>
          <Fieldset title="Outras informações">
            <KeyValue
              v-if="item.data.handicap === 'no'"
              value="Não"
              title="Possui alguma Deficiência, Transtorno Global do
                Desenvolvimento, ou Habilidades/Superdotação?" />
            <KeyValue
              v-if="item.data.handicap === 'yes'"
              value="Sim"
              title="Possui alguma Deficiência, Transtorno Global do
                Desenvolvimento, ou Habilidades/Superdotação?" />
            <KeyValue
              v-if="item.data.handicap === 'unknown'"
              value="Não disponho da informação"
              title="Possui alguma Deficiência, Transtorno Global do
                Desenvolvimento, ou Habilidades/Superdotação?" />
          </Fieldset>
          <Fieldset
            v-if="item.data.handicap === 'yes' && type=='academic'"
            title="Deficiências">
            <div>
              <ul>
                <li
                  v-for="disability in item.data.disabilities"
                  :key="disability">
                  {{ disability }}
                </li>
              </ul>
            </div>
          </Fieldset>
          <Fieldset
            v-if="item.data.handicap === 'yes' && type=='academic'"
            title="Necessidades Especiais">
            <div>
              <ul>
                <li
                  v-for="specialNeed in item.data.specialNeeds"
                  :key="specialNeed">
                  {{ specialNeed }}
                </li>
              </ul>
            </div>
          </Fieldset>
          <Fieldset
            v-if="type=='finance' && item.data.plan"
            title="Plano">
            <KeyValue
              :value="item.data.plan.name"
              title="Nome" />
            <KeyValue
              :value="item.data.plan.installments"
              title="Prestações" />
            <KeyValue
              :value="item.data.plan.dueDate"
              title="Vencimento" />
            <KeyValue
              :value="item.data.plan.description"
              title="Descrição" />
            <KeyValue
              :value="item.data.plan.value"
              title="Valor" />
            <KeyValue
              :value="item.data.plan.guarantors"
              title="Fiadores" />
          </Fieldset>
          <Fieldset
            v-if="type === 'finance'"
            title="Pagamento">
            <KeyValue
              :value="item.data.paymentMethod"
              title="Meio de Pagamento" />
          </Fieldset>
          <Fieldset
            v-if="item.data.representative"
            title="Responsável Financeiro">
            <KeyValue
              :value="item.data.representative.name"
              title="Nome" />
            <KeyValue
              :value="item.data.representative.cpf"
              title="CPF" />
            <KeyValue
              :value="item.data.representative.relationship"
              title="Relacionamento com o aluno" />
            <KeyValue
              :value="item.data.representative.email"
              title="E-mail" />
            <KeyValue
              :value="item.data.representative.phoneNumber"
              title="Telefone" />
            <KeyValue
              :value="item.data.representative.landline"
              title="Telefone Fixo" />
            <KeyValue
              :value="item.data.representative.zipcode"
              title="CEP" />
            <KeyValue
              :value="item.data.representative.addressKind"
              title="Tipo" />
            <KeyValue
              :value="item.data.representative.streetAddress"
              title="Endereço" />
            <KeyValue
              :value="item.data.representative.addressNumber"
              title="Número" />
            <KeyValue
              :value="item.data.representative.complementAddress"
              title="Complemento" />
            <KeyValue
              :value="item.data.representative.neighborhood"
              title="Bairro" />
            <KeyValue
              :value="item.data.representative.city"
              title="Cidade" />
            <KeyValue
              :value="item.data.representative.state"
              title="Estado" />
          </Fieldset>
          <Fieldset
            v-if="item.data.guarantors && item.data.guarantors.length"
            title="Fiadores">
            <div
              v-for="(guarantor, index) in item.data.guarantors"
              :key="index">
              <Fieldset :title="`Fiador ${index+1}`">
                <KeyValue
                  :value="guarantor.cpf"
                  title="CPF" />
                <KeyValue
                  :value="guarantor.name"
                  title="Nome" />
                <KeyValue
                  :value="guarantor.relationship"
                  title="Relacionamento com o aluno" />
                <KeyValue
                  :value="guarantor.email"
                  title="E-mail" />
                <KeyValue
                  :value="guarantor.phoneNumber"
                  title="Telefone" />
                <KeyValue
                  :value="guarantor.landline"
                  title="Telefone Fixo" />
                <KeyValue
                  :value="guarantor.zipcode"
                  title="CEP" />
                <KeyValue
                  :value="guarantor.addressKind"
                  title="Tipo" />
                <KeyValue
                  :value="guarantor.streetAddress"
                  title="Endereço" />
                <KeyValue
                  :value="guarantor.addressNumber"
                  title="Número" />
                <KeyValue
                  :value="guarantor.complementAddress"
                  title="Complemento" />
                <KeyValue
                  :value="guarantor.neighborhood"
                  title="Bairro" />
                <KeyValue
                  :value="guarantor.city"
                  title="Cidade" />
                <KeyValue
                  :value="guarantor.state"
                  title="Estado" />
              </Fieldset>
              <Fieldset title="Documentos">
                <div>
                  <li
                    v-for="(document, index) in guarantor.documents"
                    :key="index">
                    <a
                      :href="document.url"
                      @click.prevent="modalUrl = document.url">
                      {{ document.name }}
                    </a>
                  </li>
                </div>
              </Fieldset>
              <hr>
            </div>
          </Fieldset>
          <Fieldset
            v-if="item.data.documents && item.data.documents.length"
            title="Documentos">
            <div>
              <li
                v-for="(document, index) in item.data.documents"
                :key="index">
                <a
                  :href="document.url"
                  @click.prevent="modalUrl = document.url">
                  {{ document.name }}
                </a>
              </li>
            </div>
          </Fieldset>

          <Btn
            primary
            label="Aprovar"
            @click="approve(true)" />
          <Btn
            fab
            fixed
            primary
            @click="modal = true">
            <icon name="warning" />
          </Btn>
        </template>

        <SideModal
          v-model="modal"
          :title="$route.meta.title">
          <Fieldset
            class="m2"
            title="Pendências">
            <Multi
              v-model="pendencies"
              :errors="pendenciesErrors"
              :default="{ sectionId: null, description: '' }"
              error-key="pendencies">
              <template slot-scope="multiScope">
                <DropDown
                  v-model="multiScope.item.sectionId"
                  :errors="multiScope.error.sectionId"
                  :options="sections"
                  label="Documento" />
                <InputBox
                  v-model="multiScope.item.description"
                  :errors="multiScope.error.description"
                  label="Descrição da Pendência" />
              </template>
            </Multi>
          </Fieldset>
          <template slot="footer">
            <Btn
              primary
              label="Enviar Pendência"
              @click="approve" />
          </template>
        </SideModal>
      </div>
    </DataForm>

    <ThumbModal :url="modalUrl" />
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: "Approval",
    props: {
      id: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        modal: false,
        modalUrl: null,
        sections: [],
        pendencies: [],
        pendenciesErrors: {},
      };
    },
    computed: {
      type() {
        return this.$route.meta.type;
      },
      endpoint() {
        return [
          this.$store.getters.onboardingEndpoint,
          this.$route.meta.endpoint,
        ].join("");
      },
    },
    async mounted() {
      const { onboardingEndpoint, token } = this.$store.getters;
      const url = `${onboardingEndpoint}/v2/Approvals`;
      const headers = { Authorization: `Bearer ${token}` };
      const response = await axios.options(url, { headers });
      this.sections = response.data.data;
    },
    methods: {
      async approve(none = false) {
        const data = {
          enrollmentNumber: this.id,
          pendencies: none ? [] : this.pendencies,
        };
        await axios.post(`${this.endpoint}/${this.id}`, data);
        this.notify("Enviado com sucesso!");
        this.modal = false;
        this.$router.push("/Approvals");
      },
    },
  };
</script>
