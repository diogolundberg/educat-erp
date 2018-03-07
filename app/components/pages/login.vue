<template>
  <div class="fixed fill flex bg-dim">
    <div class="m-auto width-2 shadow2 bg-white p2 flex flex-column rounded">
      <div class="h1 thin">CMMG</div>
      <InputBox
        v-model="item.login"
        label="Login" />
      <InputBox
        v-model="item.password"
        label="Senha"
        password />
      <Btn
        primary
        label="OK"
        @click="login" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Login",
    data() {
      return {
        item: {
          login: "",
          password: "",
        },
      };
    },
    methods: {
      login() {
        localStorage.token = this.item.login;
        this.$router.replace("/enroll");
      },
    },
  };
</script>
