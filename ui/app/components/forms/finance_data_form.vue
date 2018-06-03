<template>
  <div>
    <BaseErrors
      v-model="messages" />
    <Fieldset title="Dados Financeiros">
      <List
        v-model="options.plans"
        :columns="[
          {name: 'name', title: 'Plano'},
          {name: 'value', title: 'Total', css:'right-align'},
          {name: 'installments', title: 'Parcelas', css:'right-align'},
          {name: 'installmentValue', title: 'Valor da Parcela'},
          {name: 'dueDate', title: 'Vencimento'},
          {name: 'guarantors', title: 'Fiadores'},
        ]"
        :show-filter="false"
        :disabled="!data.editable"
        @click="data.planId = $event.id">
        <template
          slot="column-name"
          scope="{ row }">
          <Radio
            :value="data.planId === row.id"
            class="mr2" />
          {{ row.name }}
        </template>
      </List>
      <DropDown
        v-model="data.paymentMethodId"
        :errors="errors.paymentMethodId"
        :size="6"
        :options="options.paymentMethod"
        :disabled="!data.editable"
        label="Meio de Pagamento" />
    </Fieldset>
    <Fieldset title="Responsável Financeiro">
      <InputBox
        v-model="data.representative.cpf"
        :errors="errors.representative && errors.representative.cpf"
        :size="4"
        :min-size="14"
        :disabled="!data.editable || !underage"
        cpf
        label="CPF"
        mask="###.###.###-##"
        hint="Ex: 000.000.000-00" />
      <InputBox
        v-model="data.representative.name"
        :errors="errors.representative && errors.representative.name"
        :size="4"
        :disabled="!data.editable || !underage"
        label="Nome completo" />
      <DropDown
        v-model="data.representative.relationshipId"
        :errors="errors.representative &&
        errors.representative.relationshipId"
        :size="4"
        :options="options.relationships"
        :disabled="!data.editable || !underage"
        label="Relacionamento com o aluno" />
    </Fieldset>
    <ContactBlock
      v-model="data.representative"
      :errors="errors.representative"
      :disabled="!data.editable || !underage" />
    <AddressBlock
      v-model="data.representative"
      :errors="errors.representative"
      :options="options"
      :disabled="!data.editable || !underage" />
    <Fieldset
      v-show="guarantorsAmount > 0"
      title="Fiadores">
      <Many
        v-model="data.guarantors"
        :errors="errors"
        :default="emptyGuarantor"
        :disabled="!data.editable"
        :amount="guarantorsAmount"
        error-key="guarantors">
        <template slot-scope="{ item, error }">
          <Fieldset
            title="Dados Gerais">
            <InputBox
              v-model="item.cpf"
              :errors="error.cpf"
              :size="4"
              :min-size="14"
              :disabled="!data.editable"
              cpf
              label="CPF"
              mask="###.###.###-##"
              hint="Ex: 000.000.000-00" />
            <InputBox
              v-model="item.name"
              :errors="error.name"
              :size="4"
              :disabled="!data.editable"
              label="Nome" />
            <DropDown
              v-model="item.relationshipId"
              :errors="error.relationshipId"
              :size="4"
              :options="options.relationships"
              :disabled="!data.editable"
              label="Relacionamento com o aluno" />
          </FieldSet>
          <AddressBlock
            v-model="item"
            :errors="error"
            :disabled="!data.editable"
            :options="options" />
          <ContactBlock
            v-model="item"
            :errors="error"
            :disabled="!data.editable" />
          <Documents
            v-model="item.documents"
            :errors="error.documents"
            :types="options.guarantorDocuments"
            :prefix="`onboarding/enrollment/${ id }/financeData/`"
            :disabled="!data.editable"
            :validations="validationsFor(item)" />
        </template>
      </Many>
    </Fieldset>
    <div class="flex justify-end">
      <Btn
        :disabled="!data.editable"
        primary
        label="Próximo"
        @click="$emit('click')" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "FinanceDataForm",
    props: {
      data: {
        type: Object,
        required: true,
      },
      errors: {
        type: Object,
        required: true,
      },
      messages: {
        type: Array,
        required: false,
        default: () => [],
      },
      options: {
        type: Object,
        required: true,
      },
      underage: {
        type: Boolean,
        required: false,
        default: false,
      },
    },
    computed: {
      guarantorsAmount() {
        const { planId } = this.data;
        const plan = this.options.plans.find(a => a.id === planId);
        return plan && plan.guarantors;
      },
    },
    methods: {
      validationsFor(item) {
        const matches = (opt, key, val) =>
          this.options[opt].filter(a => a[key]).map(a => a.id).includes(val);

        const isSpouse = matches(
          "relationships",
          "checkSpouse",
          item.relationshipId,
        );

        return [
          isSpouse && "Spouse",
        ];
      },
    },
  };
</script>
