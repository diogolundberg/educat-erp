<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="endpoint"
      title="Período de Matrícula"
      @success="success">
      <template slot-scope="{ item, errors }">
        <Fieldset title="Configuração do Horário">
          <DropDown
            v-model="item.onboardingId"
            :options="onboardings"
            :size="2"
            label="Onboarding" />
          <InputBox
            v-model="item.intervals"
            :errors="errors.intervals"
            :size="2"
            label="Intervalos" />
          <InputBox
            v-model="item.startAt"
            :errors="errors.startAt"
            :size="2"
            date
            required
            mask="##/##/####"
            label="Data de Início" />
          <InputBox
            v-model="item.scheduleStartTime"
            :errors="errors.scheduleStartTime"
            :size="2"
            required
            mask="##:##"
            label="Horário de Início" />
          <InputBox
            v-model="item.endAt"
            :errors="errors.endAt"
            :size="2"
            date
            required
            mask="##/##/####"
            label="Data de Fim" />
          <InputBox
            v-model="item.scheduleEndTime"
            :errors="errors.scheduleEndTime"
            :size="2"
            required
            mask="##:##"
            label="Horário de Fim" />
        </Fieldset>
        <Fieldset title="Pausas">
          <Multi
            v-model="item.breaks"
            :errors="errors.breaks">
            <template slot-scope="{item, error}">
              <div class="flex gutters">
                <InputBox
                  v-model="item.start"
                  :errors="error.start"
                  :size="3"
                  required
                  mask="##:##"
                  label="Horário de Início" />
                <InputBox
                  v-model="item.end"
                  :errors="error.end"
                  :size="3"
                  required
                  mask="##:##"
                  label="Horário de Fim" />
              </div>
            </template>
          </Multi>
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "SchedulingItem",
    props: {
      id: {
        type: [String, Number],
        default: null,
      },
    },
    computed: {
      endpoint() {
        return `${this.$store.getters.enrollmentUrl}/api/Scheduling`;
      },
      onboardings() {
        return this.$store.state.onboardings;
      },
    },
    async mounted() {
      await this.$store.dispatch("getOnboardingList");
    },
    methods: {
      success() {
        this.notify("Adicionado com sucesso!");
        this.$router.push("/scheduling");
      },
    },
  };
</script>
