<template>
  <div>
    <Header
      :notifications="pendencies"
      @notification="notificationClick" />

    <Spinner :active="loaded">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <PersonalDataForm :id="id" />
        </Step>
        <Step
          title="Informações da Matrícula"
          description="Saiba mais sobre seu curso">
          -
        </Step>
        <Step
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          -
        </Step>
        <Step
          title="Contrato"
          description="">
          -
        </Step>
        <Step
          title="Pagamento"
          description="">
          -
        </Step>
      </Stepper>
    </Spinner>
  </div>
</template>

<script>
  export default {
    name: "Enroll",
    props: {
      id: {
        type: [String, Number],
        required: true,
      },
    },
    data() {
      return {
        step: 1,
        pendencies: [],
      };
    },
    methods: {
      async notificationClick(anchor) {
        this.step = 1;
        await this.tick();
        await this.sleep(100);
        window.location.hash = anchor;
        this.notifications = false;
      },
    },
  };
</script>
