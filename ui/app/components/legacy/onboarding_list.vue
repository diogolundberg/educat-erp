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
    <div class="right m2">
      <router-link
        to="/onboardings/new"
        class="btn btn-primary upcase shadow0">
        Inserir Novo
      </router-link>
    </div>
  </div>
</template>

<script>
  export default {
    name: "OnboardingList",
    computed: {
      onboardings() {
        return this.$store.state.onboardings;
      },
    },
    async mounted() {
      await this.$store.dispatch("getOnboardingList");
    },
    methods: {
      show({ id }) {
        this.$router.push(`/onboardings/${id}`);
      },
    },
  };
</script>
