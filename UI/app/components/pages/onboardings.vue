<template>
  <div>
    <h2>Onboardings</h2>
    <List
      v-model="onboardings"
      :columns="[
        {name: 'semester', title: 'Semestre'},
        {name: 'year', title: 'Ano'},
        {name: 'startAt', title: 'Inicio'},
        {name: 'endAt', title: 'Fim'},
        {name: 'enrollmentCount', title: 'Matrículas'},
      ]"
      @click="show($event)" />
  </div>
</template>

<script>
  export default {
    name: "Onboardings",
    computed: {
      onboardings() {
        return this.$store.state.onboardings;
      },
    },
    async mounted() {
      await this.$store.dispatch("getOnboardingList");
    },
    methods: {
      show() {
      },
    },
  };
</script>
