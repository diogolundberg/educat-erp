<template>
  <div>
    <Fieldset title="Dados Gerais">
      <div class="col-12 center mb2">
        <img
          v-if="summary.photo"
          :src="summary.photo"
          class="rounded shadow2 x6 y6 bg-white">
        <img
          v-if="!summary.photo"
          src="../../assets/img/people.svg"
          class="rounded shadow2 x6 y6 bg-white">
      </div>
      <KeyValue
        :value="summary.name"
        title="Nome" />
      <KeyValue
        :value="summary.assumedName"
        title="Nome Social" />
      <KeyValue
        :value="summary.cpf"
        title="CPF" />
      <KeyValue
        :value="summary.birthDate"
        title="Nascimento" />
      <KeyValue
        :value="summary.maritalStatus"
        title="Estado Civil" />
      <KeyValue
        :value="summary.gender"
        title="Sexo" />
      <KeyValue
        :value="summary.nationality"
        title="Nacionalidade" />
      <KeyValue
        :value="summary.birthCountry"
        title="País de Origem" />
      <KeyValue
        :value="summary.birthCity"
        title="Naturalidade" />
      <KeyValue
        :value="summary.birthState"
        title="Estado de Nascimento" />
      <KeyValue
        :value="summary.highSchoolGraduationYear"
        title="Graduou em" />
      <KeyValue
        :value="summary.highSchoolGraduationCountry"
        title="País da Escola" />
    </Fieldset>
    <Fieldset title="Contato">
      <KeyValue
        :value="summary.email"
        title="E-Mail" />
      <KeyValue
        :value="summary.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="summary.landline"
        title="Telefone Fixo" />
    </Fieldset>
    <Fieldset title="Endereço">
      <KeyValue
        :value="summary.zipcode"
        title="CEP" />
      <KeyValue
        :value="summary.addressKind"
        title="Tipo de Endereço" />
      <KeyValue
        :value="summary.streetAddress"
        title="Logradouro" />
      <KeyValue
        :value="summary.addressNumber"
        title="Número" />
      <KeyValue
        :value="summary.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="summary.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="summary.city"
        title="Cidade" />
      <KeyValue
        :value="summary.state"
        title="Estado" />
    </Fieldset>
    <Fieldset title="Dados para o Censo">
      <KeyValue
        :value="summary.mothersName"
        title="Nome da Mãe" />
      <KeyValue
        :value="summary.race"
        title="Raça" />
      <KeyValue
        :value="summary.highSchoolKind"
        title="Tipo da Escola" />
    </Fieldset>
    <Fieldset
      v-if="summary.handicap === 'yes'"
      title="Deficiências">
      <div>
        <ul>
          <li
            v-for="disability in summary.disabilities"
            :key="disability">
            {{ disability }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset
      v-if="summary.handicap === 'yes'"
      title="Necessidades Especiais">
      <div>
        <ul>
          <li
            v-for="specialNeed in summary.specialNeeds"
            :key="specialNeed">
            {{ specialNeed }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset
      v-if="summary.plan"
      title="Plano">
      <KeyValue
        :value="summary.plan.name"
        title="Nome" />
      <KeyValue
        :value="summary.plan.installments"
        title="Prestações" />
      <KeyValue
        :value="summary.plan.dueDate"
        title="Vencimento" />
      <KeyValue
        :value="summary.plan.value"
        title="Valor" />
      <KeyValue
        :value="summary.plan.guarantors"
        title="Fiadores" />
    </Fieldset>
    <Fieldset title="Pagamento">
      <KeyValue
        :value="summary.paymentMethod"
        title="Meio de Pagamento" />
    </Fieldset>
    <Fieldset
      v-if="summary.representative"
      title="Responsável Financeiro">
      <KeyValue
        :value="summary.representative.name"
        title="Nome" />
      <KeyValue
        :value="summary.representative.cpf"
        title="CPF" />
      <KeyValue
        :value="summary.representative.relationship"
        title="Relacionamento com o aluno" />
      <KeyValue
        :value="summary.representative.email"
        title="E-mail" />
      <KeyValue
        :value="summary.representative.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="summary.representative.landline"
        title="Telefone Fixo" />
      <KeyValue
        :value="summary.representative.zipcode"
        title="CEP" />
      <KeyValue
        :value="summary.representative.addressKind"
        title="Tipo" />
      <KeyValue
        :value="summary.representative.streetAddress"
        title="Endereço" />
      <KeyValue
        :value="summary.representative.addressNumber"
        title="Número" />
      <KeyValue
        :value="summary.representative.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="summary.representative.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="summary.representative.city"
        title="Cidade" />
      <KeyValue
        :value="summary.representative.state"
        title="Estado" />
    </Fieldset>
    <Fieldset
      v-if="summary.guarantors && summary.guarantors.length"
      title="Fiadores">
      <div
        v-for="(guarantor, index) in summary.guarantors"
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
    <div class="flex justify-end">
      <Btn
        :disabled="!agreed"
        primary
        label="Enviar"
        @click="$emit('submit')" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "EnrollmentSummary",
    props: {
      summary: {
        type: Object,
        required: true,
      },
    },
    data() {
      return {
        agreed: false,
      };
    },
  };
</script>
