<template>
  <div>
    {{ meta }}
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
  import { get } from "lodash";

  export default {
    name: "ListPage",
    data() {
      return {
        records: [],
      };
    },
    watch: {
      "$route.meta"() {
        this.load();
      },
    },
    mounted() {
      this.load();
    },
    methods: {
      async load() {
        const { onboardingEndpoint, token } = this.$store.getters;
        const url = `${onboardingEndpoint}/${this.$route.meta.endpoint}`;
        const subkey = this.$route.meta.subkey || [];
        const headers = { Authorization: `Bearer ${token}` };
        const response = await axios.get(url, { headers });
        this.records = get(response, ["data", ...subkey]);
      },
      clicked(record) {
        const key = record[this.$route.meta.key || "id"];
        this.$router.push(`${this.$route.meta.link}/${key}`);
      },
    },
  };
</script>
