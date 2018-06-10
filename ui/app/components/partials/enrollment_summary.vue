<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']">
      <template slot-scope="{ item, errors }">
        <p>Envie seus dados para a secetaria e para o departamento financeiro para
        aprovação.</p>
        <div class="center">
          <img
            :style="{ 'max-width': '8rem' }"
            src="../../assets/img/card.svg">
        </div>

        <Fieldset title="Dados Gerais">
          <div class="col-12 center mb2">
            <img
              v-if="item.photo"
              :src="item.photo"
              class="rounded shadow2 x6 y6 bg-white">
            <img
              v-if="!item.photo"
              src="../../assets/img/people.svg"
              class="rounded shadow2 x6 y6 bg-white">
          </div>
          <KeyValue
            :value="item.name"
            title="Nome" />
          <KeyValue
            :value="item.assumedName"
            title="Nome Social" />
          <KeyValue
            :value="item.cpf"
            title="CPF" />
          <KeyValue
            :value="item.birthDate"
            title="Nascimento" />
          <KeyValue
            :value="item.maritalStatus"
            title="Estado Civil" />
          <KeyValue
            :value="item.gender"
            title="Sexo" />
          <KeyValue
            :value="item.nationality"
            title="Nacionalidade" />
          <KeyValue
            :value="item.birthCountry"
            title="País de Origem" />
          <KeyValue
            :value="item.birthCity"
            title="Naturalidade" />
          <KeyValue
            :value="item.birthState"
            title="Estado de Nascimento" />
          <KeyValue
            :value="item.highSchoolGraduationYear"
            title="Graduou em" />
          <KeyValue
            :value="item.highSchoolGraduationCountry"
            title="País da Escola" />
        </Fieldset>
        <Fieldset title="Contato">
          <KeyValue
            :value="item.email"
            title="E-Mail" />
          <KeyValue
            :value="item.phoneNumber"
            title="Telefone" />
          <KeyValue
            :value="item.landline"
            title="Telefone Fixo" />
        </Fieldset>
        <Fieldset title="Endereço">
          <KeyValue
            :value="item.zipcode"
            title="CEP" />
          <KeyValue
            :value="item.addressKind"
            title="Tipo de Endereço" />
          <KeyValue
            :value="item.streetAddress"
            title="Logradouro" />
          <KeyValue
            :value="item.addressNumber"
            title="Número" />
          <KeyValue
            :value="item.complementAddress"
            title="Complemento" />
          <KeyValue
            :value="item.neighborhood"
            title="Bairro" />
          <KeyValue
            :value="item.city"
            title="Cidade" />
          <KeyValue
            :value="item.state"
            title="Estado" />
        </Fieldset>
        <Fieldset title="Dados para o Censo">
          <KeyValue
            :value="item.mothersName"
            title="Nome da Mãe" />
          <KeyValue
            :value="item.race"
            title="Raça" />
          <KeyValue
            :value="item.highSchoolKind"
            title="Tipo da Escola" />
        </Fieldset>
        <Fieldset
          v-if="item.handicap === 'yes'"
          title="Deficiências">
          <div>
            <ul>
              <li
                v-for="disability in item.disabilities"
                :key="disability">
                {{ disability }}
              </li>
            </ul>
          </div>
        </Fieldset>
        <Fieldset
          v-if="item.handicap === 'yes'"
          title="Necessidades Especiais">
          <div>
            <ul>
              <li
                v-for="specialNeed in item.specialNeeds"
                :key="specialNeed">
                {{ specialNeed }}
              </li>
            </ul>
          </div>
        </Fieldset>
        <Fieldset
          v-if="item.plan"
          title="Plano">
          <KeyValue
            :value="item.plan.name"
            title="Nome" />
          <KeyValue
            :value="item.plan.installments"
            title="Prestações" />
          <KeyValue
            :value="item.plan.dueDate"
            title="Vencimento" />
          <KeyValue
            :value="item.plan.value"
            title="Valor" />
          <KeyValue
            :value="item.plan.guarantors"
            title="Fiadores" />
        </Fieldset>
        <Fieldset title="Pagamento">
          <KeyValue
            :value="item.paymentMethod"
            title="Meio de Pagamento" />
        </Fieldset>
        <Fieldset
          v-if="item.representative"
          title="Responsável Financeiro">
          <KeyValue
            :value="item.representative.name"
            title="Nome" />
          <KeyValue
            :value="item.representative.cpf"
            title="CPF" />
          <KeyValue
            :value="item.representative.relationship"
            title="Relacionamento com o aluno" />
          <KeyValue
            :value="item.representative.email"
            title="E-mail" />
          <KeyValue
            :value="item.representative.phoneNumber"
            title="Telefone" />
          <KeyValue
            :value="item.representative.landline"
            title="Telefone Fixo" />
          <KeyValue
            :value="item.representative.zipcode"
            title="CEP" />
          <KeyValue
            :value="item.representative.addressKind"
            title="Tipo" />
          <KeyValue
            :value="item.representative.streetAddress"
            title="Endereço" />
          <KeyValue
            :value="item.representative.addressNumber"
            title="Número" />
          <KeyValue
            :value="item.representative.complementAddress"
            title="Complemento" />
          <KeyValue
            :value="item.representative.neighborhood"
            title="Bairro" />
          <KeyValue
            :value="item.representative.city"
            title="Cidade" />
          <KeyValue
            :value="item.representative.state"
            title="Estado" />
        </Fieldset>
        <Fieldset
          v-if="item.guarantors && item.guarantors.length"
          title="Fiadores">
          <div
            v-for="(guarantor, index) in item.guarantors"
            :key="index"
            class="col-12">
            <Fieldset :title="`Fiador ${index+1}`">
              <KeyValue
                :value="guarantor.cpf"
                title="CPF" />
              <KeyValue
                :value="guarantor.name"
                title="Nome" />
              <KeyValue
                :value="guarantor.relationship"
                title="Relacionamento com o aluno" />
              <KeyValue
                :value="guarantor.email"
                title="E-mail" />
              <KeyValue
                :value="guarantor.phoneNumber"
                title="Telefone" />
              <KeyValue
                :value="guarantor.landline"
                title="Telefone Fixo" />
              <KeyValue
                :value="guarantor.zipcode"
                title="CEP" />
              <KeyValue
                :value="guarantor.addressKind"
                title="Tipo" />
              <KeyValue
                :value="guarantor.streetAddress"
                title="Endereço" />
              <KeyValue
                :value="guarantor.addressNumber"
                title="Número" />
              <KeyValue
                :value="guarantor.complementAddress"
                title="Complemento" />
              <KeyValue
                :value="guarantor.neighborhood"
                title="Bairro" />
              <KeyValue
                :value="guarantor.city"
                title="Cidade" />
              <KeyValue
                :value="guarantor.state"
                title="Estado" />
            </Fieldset>
          </div>
        </Fieldset>
        <Fieldset title="Confirmação">
          <Checkbox
            v-model="agreed"
            class="mb3"
            label="Confirmo que as informações acima estão corretas" />
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "EnrollmentSummary",
    props: {
      id: {
        type: [Number, String],
        required: true,
      },
    },
    data() {
      return {
        agreed: false,
      };
    },
    computed: {
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/EnrollmentSummaries`;
      },
    },
  };
</script>
