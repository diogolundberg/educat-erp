<template>
  <div class="fixed fill flex bg-dim">
    <div class="m-auto width-2 shadow2 bg-white p2 flex flex-column rounded">
      <div class="h1 thin">CMMG</div>
      <Alert
        v-if="errors && errors.base"
        :message="errors.base"
        error />
      <InputBox
        v-model="params.username"
        label="Login" />
      <InputBox
        v-model="params.password"
        label="Senha"
        type="password" />
      <Btn
        primary
        label="OK"
        @click="login" />
    </div>
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
        axios.post(`${url}/api/Token`, this.params).then((response) => {
          localStorage.token = response.data.token;
          this.$router.replace("/enroll");
        }, () => {
          this.errors.base = "Erro ao logar. Tente novamente.";
        });
      },
    },
  };
</script>
