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
      show() {
      },
    },
  };
</script>
