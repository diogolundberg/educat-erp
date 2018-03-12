<template>
  <div class="fixed fill flex bg-dim">
    <form
      class="m-auto width-2 shadow2 bg-white p2 flex flex-column rounded"
      @submit.prevent="login">
      <div class="h1 thin">CMMG</div>
      <Alert
        v-if="errors && errors.base"
        :message="errors.base"
        error />
      <InputBox
        v-model="params.username"
        required
        label="Login" />
      <InputBox
        v-model="params.password"
        required
        label="Senha"
        type="password" />
      <Submit
        label="OK"
        @click="login" />
    </form>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: "Login",
    data() {
      return {
        params: {
          username: "",
          password: "",
        },
        errors: {
          base: null,
        },
      };
    },
    methods: {
      login() {
        const url = process.env.URL1;
        return axios.post(`${url}/api/Token`, this.params).then((response) => {
          localStorage.token = response.data.token;
          this.$router.replace("/enroll");
        }, () => {
          this.errors.base = "Erro ao logar. Tente novamente.";
        });
      },
    },
  };
</script>
