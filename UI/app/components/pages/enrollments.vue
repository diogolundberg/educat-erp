<template>
  <div class="max-width-4 m-auto">
    <h2>{{ title }}</h2>
    <List
      v-model="enrollemnts"
      :columns="[{name: 'name', title: 'Nome'},{name: 'cpf', title: 'CPF'}]"
      @click="show($event)" />
    <EnrollmentInfo
      :type="type"
      ref="modal" />
  </div>
</template>

<script>
  export default {
    name: "Enrollments",
    props: {
      type: {
        type: String,
        default: "academic",
      },
    },
    computed: {
      title() {
        if (this.type === "academic") {
          return "Aprovação - Acadêmica";
        }
        return "Aprovação - Financeira";
      },
      enrollemnts() {
        if (this.type === "academic") {
          return this.$store.getters.academicApprovals;
        }
        return this.$store.getters.financeApprovals;
      },
    },
    watch: {
      type() {
        this.load();
      },
    },
    mounted() {
      this.load();
    },
    methods: {
      load() {
        if (this.type === "academic") {
          this.$store.dispatch("getAcademicApprovals");
        } else if (this.type === "finance") {
          this.$store.dispatch("getFinanceApprovals");
        }
      },
      show(enrollment) {
        this.$refs.modal.show(enrollment);
      },
    },
  };
</script>
