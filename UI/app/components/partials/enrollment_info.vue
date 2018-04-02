<template>
  <SideModal
    v-model="modal"
    :title="title">
    <div
      class="m2">
      <h2>Dados da Matrícula</h2>
      Nome: {{ enrollment.data.name }}<br>
      CPF: {{ enrollment.data.cpf }}<br>
      ID: {{ enrollment.data.enrollmentNumber }}<br>
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
</template>

<script>
  export default {
    name: "EnrollmentInfo",
    props: {
      type: {
        type: String,
        default: "academic",
      },
    },
    data() {
      return {
        modal: false,
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
    methods: {
      async show(enrollment) {
        const type = this.type === "academic" ? "Academic" : "Finance";
        await this.$store.dispatch(`get${type}ApprovalInfo`, enrollment);
        this.modal = true;
      },
      async approve() {
        await this.$store.dispatch(`${this.type}Approve`, this.enrollment.data);
        this.enrollment.messages.forEach(this.notify);
        this.modal = false;
      },
    },
  };
</script>
