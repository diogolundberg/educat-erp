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
      <h2>Pendências</h2>
      <CheckGroup
        v-model="enrollment.data.pendencies"
        :options="enrollment.options.pendencyList"
        vertical />
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
      approve(enrollment) {
        const type = this.type === "academic" ? "Academic" : "Finance";
        this.$store.dispatch(`approve${type}`, enrollment);
      },
    },
  };
</script>
