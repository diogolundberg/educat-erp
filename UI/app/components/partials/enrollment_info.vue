<template>
  <div
    v-if="loaded"
    class="max-width-4 m-auto">
    <h2>{{ title }}</h2>
    <Fieldset title="Dados Gerais">
      <div class="col-12">
        <div class="right">
          <img
            v-if="enrollment.data.photo"
            :src="enrollment.data.photo"
            class="rounded shadow2 x6 y6 bg-white">
          <img
            v-if="!enrollment.data.photo"
            src="../../assets/img/people.svg"
            class="rounded shadow2 x6 y6 bg-white">
        </div>
        <strong>CPF</strong>: {{ enrollment.data.cpf }}<br>
        <strong>Nome</strong>: {{ enrollment.data.name }}<br>
        <strong>Nome Civil</strong>: {{ enrollment.data.assumedName }}<br>
        <template v-if="type=='academic'">
          <strong>Nascimento</strong>: {{ enrollment.data.birthDate }}<br>
          <strong>Nome da Mãe</strong>: {{ enrollment.data.mothersName }}<br>
          <strong>Sexo</strong>: {{ enrollment.data.gender }}<br>
          <strong>Estado Civil</strong>: {{ enrollment.data.maritalStatus }}<br>
          <strong>Naturalidade</strong>: {{ enrollment.data.birthCity }}<br>
          <strong>Estado de Nascimento</strong>: {{ enrollment.data.birthState }}<br>
          <strong>País de Origem</strong>: {{ enrollment.data.birthCountry }}<br>
          <strong>Nacionalidade</strong>: {{ enrollment.data.nationality }}<br>
        </template>
      </div>
    </Fieldset>
    <Fieldset
      v-if="type=='academic'"
      title="Dados para o Censo">
      <div>
        <strong>Raça</strong>: {{ enrollment.data.race }}<br>
        <strong>Tipo da Escola</strong>: {{ enrollment.data.highSchollKind }}<br>
        <strong>Graduou em</strong>: {{ enrollment.data.highSchoolGraduationYear }}<br>
        <strong>País da Escola:</strong>:
        {{ enrollment.data.highSchoolGraduationCountry }}<br>
      </div>
    </Fieldset>
    <Fieldset title="Contato">
      <div>
        <strong>E-Mail</strong>: {{ enrollment.data.email }}<br>
        <strong>Telefone</strong>: {{ enrollment.data.phoneNumber }}<br>
        <strong>Telefone Fixo</strong>: {{ enrollment.data.landline }}<br>
      </div>
    </Fieldset>
    <Fieldset
      v-if="type=='academic'"
      title="Endereço">
      <div>
        <strong>CEP</strong>: {{ enrollment.data.zipcode }}<br>
        <strong>Logradouro</strong>: {{ enrollment.data.streetAddress }}<br>
        <strong>Tipo</strong>: {{ enrollment.data.addressKind }}<br>
        <strong>Complemento</strong>: {{ enrollment.data.complementAddress }}<br>
        <strong>Bairro</strong>: {{ enrollment.data.neighborhood }}<br>
        <strong>Cidade</strong>: {{ enrollment.data.city }}<br>
        <strong>Estado</strong>: {{ enrollment.data.state }}<br>
      </div>
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
      <div>
        <strong>Nome</strong>: {{ enrollment.data.plan.name }}<br>
        <strong>Prestações</strong>: {{ enrollment.data.plan.installments }}<br>
        <strong>Vencimento</strong>: {{ enrollment.data.plan.dueDate }}<br>
        <strong>Descrição</strong>: {{ enrollment.data.plan.description }}<br>
        <strong>Valor</strong>: {{ enrollment.data.plan.value }}<br>
        <strong>Fiadores</strong>: {{ enrollment.data.plan.guarantors }}<br>
      </div>
    </Fieldset>
    <Fieldset title="Pagamento">
      <div>
        <strong>Meio de Pagamento</strong>: {{ enrollment.data.paymentMethod }}<br>
      </div>
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.representative"
      title="Responsável Financeiro">
      <div>
        <strong>CPF</strong>: {{ enrollment.data.representative.cpf }}<br>
        <strong>Nome</strong>: {{ enrollment.data.representative.name }}<br>
        <strong>E-mail</strong>: {{ enrollment.data.representative.email }}<br>
        <strong>Telefone</strong>: {{ enrollment.data.representative.phoneNumber }}<br>
        <strong>Telefone Fixo</strong>: {{ enrollment.data.representative.landline }}<br>
        <strong>CEP</strong>: {{ enrollment.data.representative.zipcode }}<br>
        <strong>Tipo</strong>: {{ enrollment.data.representative.addressKind }}<br>
        <strong>Endereço</strong>: {{ enrollment.data.representative.streetAddress }}<br>
        <strong>Complemento</strong>:
        {{ enrollment.data.representative.complementAddress }}<br>
        <strong>Bairro</strong>: {{ enrollment.data.representative.neighborhood }}<br>
        <strong>Cidade</strong>: {{ enrollment.data.representative.city }}<br>
        <strong>Estado</strong>: {{ enrollment.data.representative.state }}<br>
      </div>
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.guarantors && enrollment.data.guarantors.length"
      title="Fiadores">
      <div
        v-for="(guarantor, index) in enrollment.data.guarantors"
        :key="index">
        <Fieldset :title="`Fiador ${index+1}`">
          <div>
            <strong>CPF</strong>: {{ guarantor.cpf }}<br>
            <strong>Nome</strong>: {{ guarantor.name }}<br>
            <strong>E-mail</strong>: {{ guarantor.email }}<br>
            <strong>Telefone</strong>: {{ guarantor.phoneNumber }}<br>
            <strong>Telefone Fixo</strong>: {{ guarantor.landline }}<br>
            <strong>CEP</strong>: {{ guarantor.zipcode }}<br>
            <strong>Tipo</strong>: {{ guarantor.addressKind }}<br>
            <strong>Endereço</strong>: {{ guarantor.streetAddress }}<br>
            <strong>Complemento</strong>: {{ guarantor.complementAddress }}<br>
            <strong>Bairro</strong>: {{ guarantor.neighborhood }}<br>
            <strong>Cidade</strong>: {{ guarantor.city }}<br>
            <strong>Estado</strong>: {{ guarantor.state }}<br>
          </div>
        </Fieldset>
        <Fieldset title="Documentos">
          <div>
            <li
              v-for="document in guarantor.documents"
              :key="document.url">
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
          v-for="document in enrollment.data.documents"
          :key="document.url">
          <a
            href="#"
            @click.prevent="modalUrl = document.url">
            {{ document.name }}
          </a>
        </li>
      </div>
    </Fieldset>

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
      :title="title">
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
          label="Aprovar"
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
    },
    async mounted() {
      const type = this.type === "academic" ? "Academic" : "Finance";
      await this.$store.dispatch(`get${type}ApprovalInfo`, this.id);
      this.loaded = true;
    },
    methods: {
      async approve() {
        await this.$store.dispatch(`${this.type}Approve`, this.enrollment.data);
        this.notify("Enviado com sucesso!");
        this.modal = false;
        this.$router.push(`/enrollments/${this.type}`);
      },
    },
  };
</script>
