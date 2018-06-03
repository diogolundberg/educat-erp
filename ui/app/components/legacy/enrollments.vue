<template>
  <div class="max-width-4 m-auto">
    <h2>{{ title }}</h2>
    <List
      v-model="enrollments"
      :columns="[
        {name: 'name', title: 'Nome'},
        {name: 'cpf', title: 'CPF'},
        {name: 'birthDate', title: 'Nascimento'},
        {name: 'email', title: 'E-mail'},
        {name: 'phoneNumber', title: 'Celular'},
        {name: 'plan', title: 'Plano', show: type === 'finance'},
        {name: 'updatedAt', title: 'Modificado em'},
        {name: 'status', title: 'Status'},
      ]"
      @click="show($event)" />
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
        const type = this.type === "academic" ? "Acadêmica" : "Financeira";
        return `Aprovação - ${type}`;
      },
      enrollments() {
        return this.$store.getters[`${this.type}Approvals`];
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
        const type = this.type === "academic" ? "Academic" : "Finance";
        this.$store.dispatch(`get${type}Approvals`);
      },
      show({ enrollmentNumber }) {
        this.$router.push(`/enrollments/${this.type}/${enrollmentNumber}`);
      },
    },
  };
</script>
