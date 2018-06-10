<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']">
      <template slot-scope="{ item, errors }">
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
            :disabled="disabled"
            @click="item.planId = $event.id">
            <template
              slot="column-name"
              scope="{ row }">
              <Radio
                :value="item.planId === row.id"
                class="mr2" />
              {{ row.name }}
            </template>
          </List>
          <DropDown
            v-model="item.paymentMethodId"
            :errors="errors.paymentMethodId"
            :size="6"
            :options="options.paymentMethod"
            :disabled="disabled"
            label="Meio de Pagamento" />
        </Fieldset>
        <Fieldset
          v-show="guarantorsAmountFor(item) > 0"
          title="Fiadores">
          <Many
            v-model="item.guarantors"
            :errors="errors"
            :default="emptyGuarantor"
            :disabled="disabled"
            :amount="guarantorsAmountFor(item)"
            error-key="guarantors">
            <template slot-scope="{ item, error }">
              <Fieldset
                title="Dados Gerais">
                <InputBox
                  v-model="item.cpf"
                  :errors="error.cpf"
                  :size="4"
                  :min-size="14"
                  :disabled="disabled"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="item.name"
                  :errors="error.name"
                  :size="4"
                  :disabled="disabled"
                  label="Nome" />
                <DropDown
                  v-model="item.relationshipId"
                  :errors="error.relationshipId"
                  :size="4"
                  :options="options.relationships"
                  :disabled="disabled"
                  label="Relacionamento com o aluno" />
              </FieldSet>
              <AddressBlock
                v-model="item"
                :errors="error"
                :disabled="disabled"
                :options="options" />
              <ContactBlock
                v-model="item"
                :errors="error"
                :disabled="disabled" />
              <Documents
                v-model="item.documents"
                :errors="error.documents"
                :types="options.guarantorDocuments"
                :prefix="`onboarding/enrollment/${ id }/financeData/`"
                :disabled="disabled"
                :validations="validationsFor(item)" />
            </template>
          </Many>
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "FinanceDataForm",
    props: {
      id: {
        type: [Number, String],
        required: true,
      },
      disabled: {
        type: Boolean,
        required: false,
        default: false,
      },
      options: {
        type: Object,
        required: true,
      },
    },
    computed: {
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/FinanceDatas`;
      },
      emptyGuarantor() {
        return {
          discriminator: "RepresentativePerson",
          cpf: "",
          cnpj: "",
          name: "",
          contact: "",
          relationshipId: null,
          streetAddress: "",
          complementAddress: "",
          neighborhood: "",
          phoneNumber: "",
          landline: "",
          email: "",
          cityId: null,
          stateId: null,
        };
      },
    },
    methods: {
      guarantorsAmountFor({ planId }) {
        const plan = this.options.plans.find(a => a.id === planId);
        return plan && plan.guarantors;
      },
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
