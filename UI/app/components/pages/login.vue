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
      async login() {
        try {
          await this.$store.dispatch("login", this.params);
          this.$router.replace("/enrollments");
        } catch (e) {
          this.errors.base = "Erro ao logar. Tente novamente.";
        }
      },
    },
  };
</script>
