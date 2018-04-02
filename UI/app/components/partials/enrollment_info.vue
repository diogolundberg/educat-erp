<template>
  <SideModal
    v-model="modal"
    :title="title">
    <div
      v-if="$store.getters.enrollmentInfo"
      class="m2">
      Nome: {{ $store.getters.enrollmentInfo.name }}<br>
      CPF: {{ $store.getters.enrollmentInfo.cpf }}<br>
      ID: {{ $store.getters.enrollmentInfo.enrollmentNumber }}<br>
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
      title() {
        const type = this.type === "academic" ? "Acadêmica" : "Financeira";
        return `Aprovação - ${type}`;
      },
    },
    methods: {
      show(enrollment) {
        const type = this.type === "academic" ? "Academic" : "Finance";
        this.$store.dispatch(`get${type}ApprovalInfo`, enrollment);
        this.modal = true;
      },
      approve(enrollment) {
        const type = this.type === "academic" ? "Academic" : "Finance";
        this.$store.dispatch(`approve${type}`, enrollment);
      },
    },
  };
</script>
