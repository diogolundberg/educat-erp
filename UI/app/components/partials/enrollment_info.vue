<template>
  <div class="max-width-4 m-auto">
    <h2>{{ title }}</h2>
    <Fieldset
      title="Dados Gerais">
      CPF: {{ enrollment.data.cpf }}
    </Fieldset>
    <div class="flex justify-end">
      <Btn
        primary
        label="Aprovar/Reprovar"
        @click="modal = true" />
    </div>

    <SideModal
      v-model="modal"
      :title="title">
      <div
        class="m2">
        <h2>Dados da Matrícula</h2>
        Nome: {{ enrollment.data.name }}<br>
        CPF: {{ enrollment.data.cpf }}<br>
        ID: {{ enrollment.data.enrollmentNumber }}<br>
        <Fieldset title="Pendências">
          <Multi
            v-model="enrollment.data.pendencies"
            :errors="enrollment.errors"
            :default="{ sectionId: null, description: '' }"
            :error-key="pendencies">
            <template slot-scope="{ item, error }">
              <DropDown
                v-model="item.sectionId"
                :errors="error.sectionId"
                :options="enrollment.options.sections"
                label="Documento" />
              <InputBox
                v-model="item.description"
                :errors="error.description"
                label="Descrição" />
            </template>
          </Multi>
        </Fieldset>
      </div>
      <template slot="footer">
        <Btn
          primary
          label="Aprovar"
          @click="approve" />
      </template>
    </SideModal>
  </div>
</template>

<script>
  export default {
    name: "EnrollmentInfo",
    props: {
      type: {
        type: String,
        default: "academic",
      },
      id: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        modal: false,
      };
    },
    computed: {
      enrollment() {
        return this.$store.state.enrollmentInfo;
      },
      title() {
        const type = this.type === "academic" ? "Acadêmica" : "Financeira";
        return `Aprovação - ${type}`;
      },
    },
    async mounted() {
      const type = this.type === "academic" ? "Academic" : "Finance";
      await this.$store.dispatch(`get${type}ApprovalInfo`, this.id);
    },
    methods: {
      async approve() {
        await this.$store.dispatch(`${this.type}Approve`, this.enrollment.data);
        this.notify("Enviado com sucesso!");
        this.modal = false;
      },
    },
  };
</script>
