<template>
  <div>
    <h2>Agendamentos</h2>
    <List
      v-model="schedulingList"
      :columns="[
        {name: 'onboarding', title: 'Onboarding'},
        {name: 'startAt', title: 'Inicio'},
        {name: 'endAt', title: 'Fim'},
      ]"
      :default-filters="defaultFilters"
      @click="show($event)">
      <template slot-scope="{ filters }">
        <DropDown
          v-model="filters.onboarding"
          :options="onboardingList"
          class="ml1"
          id-field="name"
          label="Onboarding" />
      </template>
    </List>
    <div class="right m2">
      <router-link
        to="/scheduling/new"
        class="btn btn-primary upcase shadow0">
        Inserir Novo
      </router-link>
    </div>
  </div>
</template>

<script>
  export default {
    name: "SchedulingList",
    params: {
      onboarding: {
        type: String,
        default: null,
      },
    },
    computed: {
      schedulingList() {
        return this.$store.state.schedulingList;
      },
      onboardingList() {
        return this.$store.state.onboardings;
      },
      defaultFilters() {
        const { onboarding } = this.$route.query;
        return onboarding && { onboarding };
      },
    },
    async mounted() {
      await this.$store.dispatch("getOnboardingList");
      await this.$store.dispatch("getSchedulingList");
    },
    methods: {
      show({ id }) {
        this.$router.push(`/scheduling/${id}`);
      },
    },
  };
</script>
