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
          :status="getStep('PersonalDatas').status"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <PersonalDataForm
            :id="id"
            :options="options" />
        </Step>
        <Step
          :status="getStep('CourseSummaries').status"
          title="Informações da Matrícula"
          description="Saiba mais sobre seu curso">
          <InfoForm />
        </Step>
        <Step
          :status="getStep('FinanceDatas').status"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <FinanceDataForm
            :id="id"
            :options="options" />
        </Step>
        <Step
          :status="getStep('EnrollmentSummaries').status"
          title="Resumo"
          description="">
          <EnrollmentSummary
            :id="id" />
        </Step>
        <Step
          :status="getStep('Contracts').status"
          title="Contrato"
          description="">
          <ContractForm
            :id="id" />
        </Step>
        <Step
          :status="getStep('Payments').status"
          title="Pagamento"
          description="">
          -
        </Step>
      </Stepper>
    </Spinner>
  </div>
</template>

<script>
  import axios from "axios";

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
        record: {
          steps: [],
        },
        pendencies: [],
        options: {
          plans: [],
        },
      };
    },
    async mounted() {
      const { onboardingEndpoint } = this.$store.getters;
      const url = `${onboardingEndpoint}/v2/Enrollments/${this.id}`;
      const response = await axios.get(url);
      this.record = response.data.data;
    },
    methods: {
      getStep(resource) {
        return this.record.steps.find(a => a.resource === resource) || {};
      },
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
