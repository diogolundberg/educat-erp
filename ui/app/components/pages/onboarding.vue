<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="endpoint"
      :sub-key="['data']"
      title="Período de Matrícula"
      @success="success">
      <template slot-scope="{ item, errors }">
        <Fieldset title="Configuração de Período">
          <InputBox
            v-model="item.year"
            :errors="errors.year"
            :size="3"
            mask="####"
            label="Ano" />
          <InputBox
            v-model="item.semester"
            :errors="errors.semester"
            :size="3"
            mask="#"
            label="Semestre" />
          <InputBox
            v-model="item.startAt"
            :errors="errors.startAt"
            :size="3"
            date
            required
            mask="##/##/####"
            label="Data de Início" />
          <InputBox
            v-model="item.endAt"
            :errors="errors.endAt"
            :size="3"
            date
            required
            mask="##/##/####"
            label="Data de Fim" />
        </Fieldset>
        <Fieldset title="Alunos">
          <Multi
            v-model="item.enrollments"
            :errors="errors"
            :default="defaultEnrollment"
            :error-key="enrollments">
            <template slot-scope="{item, error}">
              <div class="flex gutters">
                <InputBox
                  v-model="item.name"
                  :errors="error.name"
                  :size="4"
                  required
                  label="Nome"
                  hint="Nome completo" />
                <InputBox
                  v-model="item.email"
                  :errors="error.email"
                  :size="4"
                  email
                  required
                  label="E-mail" />
                <InputBox
                  v-model="item.cpf"
                  :errors="error.cpf"
                  :size="4"
                  :min-size="14"
                  required
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
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
    name: "Onboarding",
    props: {
      id: {
        type: [String, Number],
        default: null,
      },
    },
    computed: {
      endpoint() {
        return `${this.$store.getters.onboardingEndpoint}/v2/Onboardings`;
      },
      defaultEnrollment() {
        return {
          name: "",
          cpf: "",
          email: "",
        };
      },
    },
    methods: {
      success() {
        this.notify("Adicionado com sucesso!");
        this.$router.push("/onboarding");
      },
    },
  };
</script>
