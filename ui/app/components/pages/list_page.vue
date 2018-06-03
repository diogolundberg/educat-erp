<template>
  <div>
    <h2>{{ $route.meta.title }}</h2>
    <List
      :value="records"
      :columns="$route.meta.columns"
      @click="clicked" />
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: "ListPage",
    data() {
      return {
        records: [],
      };
    },
    async mounted() {
      const { onboardingEndpoint, token } = this.$store.getters;
      const url = `${onboardingEndpoint}/${this.$route.meta.endpoint}`;
      const headers = { Authorization: `Bearer ${token}` };
      const response = await axios.get(url, { headers });
      this.records = response.data.records;
    },
    methods: {
      clicked({ id }) {
        this.$router.push(`${this.$route.meta.link}/${id}`);
      },
    },
  };
</script>
