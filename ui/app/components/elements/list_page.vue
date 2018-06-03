<template>
  <div>
    <h2>{{ $route.meta.title }}</h2>
    <List
      :value="records"
      :columns="$route.meta.columns"
      @click="clicked" />
    <div
      v-if="$route.meta.new"
      class="right m2">
      <router-link
        :to="$route.meta.new"
        class="btn btn-primary upcase shadow0">
        Inserir Novo
      </router-link>
    </div>
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
      clicked(record) {
        const key = record[this.$route.meta.key || "id"];
        this.$router.push(`${this.$route.meta.link}/${key}`);
      },
    },
  };
</script>
