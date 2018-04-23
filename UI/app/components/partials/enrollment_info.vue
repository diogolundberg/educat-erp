<template>
  <div
    v-if="loaded"
    class="max-width-4 m-auto">
    <h2>{{ title }}</h2>
    <Fieldset title="Dados Gerais">
      <div class="col-12 center mb2">
        <img
          v-if="enrollment.data.photo"
          :src="enrollment.data.photo"
          class="rounded shadow2 x6 y6 bg-white">
        <img
          v-if="!enrollment.data.photo"
          src="../../assets/img/people.svg"
          class="rounded shadow2 x6 y6 bg-white">
      </div>
      <KeyValue
        :value="enrollment.data.name"
        title="Nome" />
      <KeyValue
        :value="enrollment.data.assumedName"
        title="Nome Social" />
      <KeyValue
        :value="enrollment.data.cpf"
        title="CPF" />
      <template v-if="type=='academic'">
        <KeyValue
          :value="enrollment.data.birthDate"
          title="Nascimento" />
        <KeyValue
          :value="enrollment.data.maritalStatus"
          title="Estado Civil" />
        <KeyValue
          :value="enrollment.data.gender"
          title="Sexo" />
        <KeyValue
          :value="enrollment.data.nationality"
          title="Nacionalidade" />
        <KeyValue
          :value="enrollment.data.birthCountry"
          title="País de Origem" />
        <KeyValue
          :value="enrollment.data.birthCity"
          title="Naturalidade" />
        <KeyValue
          :value="enrollment.data.birthState"
          title="Estado de Nascimento" />
        <KeyValue
          :value="enrollment.data.highSchoolGraduationYear"
          title="Graduou em" />
        <KeyValue
          :value="enrollment.data.highSchoolGraduationCountry"
          title="País da Escola" />
      </template>
    </Fieldset>
    <Fieldset title="Contato">
      <KeyValue
        :value="enrollment.data.email"
        title="E-Mail" />
      <KeyValue
        :value="enrollment.data.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="enrollment.data.landline"
        title="Telefone Fixo" />
    </Fieldset>
    <Fieldset
      v-if="type=='academic'"
      title="Endereço">
      <KeyValue
        :value="enrollment.data.zipcode"
        title="CEP" />
      <KeyValue
        :value="enrollment.data.addressKind"
        title="Tipo de Endereço" />
      <KeyValue
        :value="enrollment.data.streetAddress"
        title="Logradouro" />
      <KeyValue
        :value="enrollment.data.addressNumber"
        title="Número" />
      <KeyValue
        :value="enrollment.data.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="enrollment.data.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="enrollment.data.city"
        title="Cidade" />
      <KeyValue
        :value="enrollment.data.state"
        title="Estado" />
    </Fieldset>
    <Fieldset
      v-if="type=='academic'"
      title="Dados para o Censo">
      <KeyValue
        :value="enrollment.data.mothersName"
        title="Nome da Mãe" />
      <KeyValue
        :value="enrollment.data.race"
        title="Raça" />
      <KeyValue
        :value="enrollment.data.highSchollKind"
        title="Tipo da Escola" />
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.handicap === 'yes' && type=='academic'"
      title="Deficiências">
      <div>
        <ul>
          <li
            v-for="disability in enrollment.data.disabilities"
            :key="disability">
            {{ disability }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.handicap === 'yes' && type=='academic'"
      title="Necessidades Especiais">
      <div>
        <ul>
          <li
            v-for="specialNeed in enrollment.data.specialNeeds"
            :key="specialNeed">
            {{ specialNeed }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset
      v-if="type=='finance' && enrollment.data.plan"
      title="Plano">
      <KeyValue
        :value="enrollment.data.plan.name"
        title="Nome" />
      <KeyValue
        :value="enrollment.data.plan.installments"
        title="Prestações" />
      <KeyValue
        :value="enrollment.data.plan.dueDate"
        title="Vencimento" />
      <KeyValue
        :value="enrollment.data.plan.description"
        title="Descrição" />
      <KeyValue
        :value="enrollment.data.plan.value"
        title="Valor" />
      <KeyValue
        :value="enrollment.data.plan.guarantors"
        title="Fiadores" />
    </Fieldset>
    <Fieldset
      v-if="type === 'finance'"
      title="Pagamento">
      <KeyValue
        :value="enrollment.data.paymentMethod"
        title="Meio de Pagamento" />
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.representative"
      title="Responsável Financeiro">
      <KeyValue
        :value="enrollment.data.representative.name"
        title="Nome" />
      <KeyValue
        :value="enrollment.data.representative.cpf"
        title="CPF" />
      <KeyValue
        :value="enrollment.data.representative.relationship"
        title="Relacionamento com o aluno" />
      <KeyValue
        :value="enrollment.data.representative.email"
        title="E-mail" />
      <KeyValue
        :value="enrollment.data.representative.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="enrollment.data.representative.landline"
        title="Telefone Fixo" />
      <KeyValue
        :value="enrollment.data.representative.zipcode"
        title="CEP" />
      <KeyValue
        :value="enrollment.data.representative.addressKind"
        title="Tipo" />
      <KeyValue
        :value="enrollment.data.representative.streetAddress"
        title="Endereço" />
      <KeyValue
        :value="enrollment.data.representative.addressNumber"
        title="Número" />
      <KeyValue
        :value="enrollment.data.representative.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="enrollment.data.representative.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="enrollment.data.representative.city"
        title="Cidade" />
      <KeyValue
        :value="enrollment.data.representative.state"
        title="Estado" />
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.guarantors && enrollment.data.guarantors.length"
      title="Fiadores">
      <div
        v-for="(guarantor, index) in enrollment.data.guarantors"
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
                href="#"
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
      v-if="enrollment.data.documents && enrollment.data.documents.length"
      title="Documentos">
      <div>
        <li
          v-for="(document, index) in enrollment.data.documents"
          :key="index">
          <a
            href="#"
            @click.prevent="modalUrl = document.url">
            {{ document.name }}
          </a>
        </li>
      </div>
    </Fieldset>
    <div class="center">
      <Btn
        primary
        label="Aprovar"
        @click="approve(true)" />
    </div>

    <div class="flex justify-end m3">
      <Btn
        fab
        fixed
        primary
        @click="modal = true">
        <icon name="warning" />
      </Btn>
    </div>

    <Modal
      v-if="!!modalUrl"
      class="fit-height"
      @hide="modalUrl = null">
      <img
        v-if="!!modalUrl.match('(jpe?g|png)$')"
        :src="modalUrl"
        class="col-12 fit">
      <iframe
        v-else
        :src="modalUrl"
        height="600"
        class="col-12 fit border-none" />
    </Modal>

    <SideModal
      v-model="modal"
      :title="modalTitle">
      <div
        class="m2">
        <Fieldset title="Pendências">
          <Multi
            v-model="enrollment.data.pendencies"
            :errors="enrollment.errors"
            :default="{ sectionId: null, description: '' }"
            :error-key="pendencies">
            <template slot-scope="{ item, error }">
              <DropDown
                v-model="item.sectionId"
                :errors="error.sectionId"
                :options="enrollment.options.sections"
                label="Documento" />
              <InputBox
                v-model="item.description"
                :errors="error.description"
                label="Descrição" />
            </template>
          </Multi>
        </Fieldset>
      </div>
      <template slot="footer">
        <Btn
          primary
          label="Enviar"
          @click="approve" />
      </template>
    </SideModal>
  </div>
</template>

<script>
  export default {
    name: "EnrollmentInfo",
    props: {
      type: {
        type: String,
        default: "academic",
      },
      id: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        loaded: false,
        modal: false,
        modalUrl: null,
      };
    },
    computed: {
      enrollment() {
        return this.$store.state.enrollmentInfo;
      },
      title() {
        const type = this.type === "academic" ? "Acadêmica" : "Financeira";
        return `Aprovação - ${type}`;
      },
      modalTitle() {
        const type = this.type === "academic" ? "Acadêmica" : "Financeira";
        return `Pendência ${type}`;
      },
    },
    async mounted() {
      const type = this.type === "academic" ? "Academic" : "Finance";
      await this.$store.dispatch(`get${type}ApprovalInfo`, this.id);
      this.loaded = true;
    },
    methods: {
      async approve(removePendencies = false) {
        if (removePendencies) {
          this.enrollment.data.pendencies.splice(0, 9999);
        }
        await this.$store.dispatch(`${this.type}Approve`, this.enrollment.data);
        this.notify("Enviado com sucesso!");
        this.modal = false;
        this.$router.push(`/enrollments/${this.type}`);
      },
    },
  };
</script>
