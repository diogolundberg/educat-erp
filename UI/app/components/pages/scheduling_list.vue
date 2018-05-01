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
      @click="show($event)" />
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
    computed: {
      schedulingList() {
        return this.$store.state.schedulingList;
      },
    },
    async mounted() {
      await this.$store.dispatch("getSchedulingList");
    },
    methods: {
      show({ id }) {
        this.$router.push(`/scheduling/${id}`);
      },
    },
  };
</script>
