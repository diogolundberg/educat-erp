<template>
  <div>
    <Header />
    <List
      v-model="$store.getters.academicApprovals"
      :columns="[{name: 'name', title: 'Nome'},{name: 'cpf', title: 'CPF'}]"
      @click="show($event)" />
  </div>
</template>

<script>
  export default {
    name: "Enrollments",
    async mounted() {
      await this.$store.dispatch("getAcademicApprovals");
    },
    methods: {
      show({ enrollmentNumber }) {
        this.notify(enrollmentNumber);
      },
    },
  };
</script>
