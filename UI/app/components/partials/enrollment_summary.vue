<template>
  <div>
    <Fieldset title="Dados Gerais">
      <div class="col-12 center mb2">
        <img
          v-if="enrollment.data.photo"
          :src="enrollment.data.photo"
          class="rounded shadow2 x6 y6 bg-white">
        <img
          v-if="!enrollment.data.photo"
          src="../../assets/img/people.svg"
          class="rounded shadow2 x6 y6 bg-white">
      </div>
      <KeyValue
        :value="enrollment.data.personalData.realName"
        title="Nome" />
      <KeyValue
        :value="enrollment.data.personalData.assumedName"
        title="Nome Social" />
      <KeyValue
        :value="enrollment.data.personalData.cpf"
        title="CPF" />
      <KeyValue
        :value="enrollment.data.personalData.birthDate"
        title="Nascimento" />
      <KeyValue
        :value="getOption('maritalStatuses', 'maritalStatusId')"
        title="Estado Civil" />
      <KeyValue
        :value="getOption('genders', 'genderId')"
        title="Sexo" />
      <KeyValue
        :value="getOption('nationalities', 'nationalityId')"
        title="Nacionalidade" />
      <KeyValue
        :value="getOption('countries', 'birthCountryId')"
        title="País de Origem" />
      <KeyValue
        :value="getOption('cities', 'birthCityId')"
        title="Naturalidade" />
      <KeyValue
        :value="getOption('states', 'birthStateId')"
        title="Estado de Nascimento" />
      <KeyValue
        :value="enrollment.data.personalData.highSchoolGraduationYear"
        title="Graduou em" />
      <KeyValue
        :value="getOption('countries', 'highSchoolGraduationCountryId')"
        title="País da Escola" />
    </Fieldset>
    <Fieldset title="Contato">
      <KeyValue
        :value="enrollment.data.personalData.email"
        title="E-Mail" />
      <KeyValue
        :value="enrollment.data.personalData.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="enrollment.data.personalData.landline"
        title="Telefone Fixo" />
    </Fieldset>
    <Fieldset title="Endereço">
      <KeyValue
        :value="enrollment.data.personalData.zipcode"
        title="CEP" />
      <KeyValue
        :value="getOption('addressKinds', 'addressKindId')"
        title="Tipo de Endereço" />
      <KeyValue
        :value="enrollment.data.personalData.streetAddress"
        title="Logradouro" />
      <KeyValue
        :value="enrollment.data.personalData.addressNumber"
        title="Número" />
      <KeyValue
        :value="enrollment.data.personalData.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="enrollment.data.personalData.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="getOption('cities', 'cityId')"
        title="Cidade" />
      <KeyValue
        :value="getOption('states', 'stateId')"
        title="Estado" />
    </Fieldset>
    <Fieldset title="Dados para o Censo">
      <KeyValue
        :value="enrollment.data.personalData.mothersName"
        title="Nome da Mãe" />
      <KeyValue
        :value="getOption('races', 'raceId')"
        title="Raça" />
      <KeyValue
        :value="getOption('highSchoolKinds', 'highSchoolKindId')"
        title="Tipo da Escola" />
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.personalData.handicap === 'yes'"
      title="Deficiências">
      <div>
        <ul>
          <li
            v-for="disability in enrollment.data.personalData.disabilities"
            :key="disability">
            {{ disability }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.personalData.handicap === 'yes'"
      title="Necessidades Especiais">
      <div>
        <ul>
          <li
            v-for="specialNeed in enrollment.data.personalData.specialNeeds"
            :key="specialNeed">
            {{ specialNeed }}
          </li>
        </ul>
      </div>
    </Fieldset>
    <Fieldset title="Plano">
      <KeyValue
        :value="getOptionFinance('plans', 'planId', 'name')"
        title="Nome" />
      <KeyValue
        :value="getOptionFinance('plans', 'planId', 'installments')"
        title="Prestações" />
      <KeyValue
        :value="getOptionFinance('plans', 'planId', 'dueDate')"
        title="Vencimento" />
      <KeyValue
        :value="getOptionFinance('plans', 'planId', 'value')"
        title="Valor" />
      <KeyValue
        :value="getOptionFinance('plans', 'planId', 'guarantors')"
        title="Fiadores" />
    </Fieldset>
    <Fieldset title="Pagamento">
      <KeyValue
        :value="getOptionFinance('paymentMethod', 'paymentMethodId')"
        title="Meio de Pagamento" />
    </Fieldset>
    <Fieldset title="Responsável Financeiro">
      <KeyValue
        :value="enrollment.data.financeData.representative.name"
        title="Nome" />
      <KeyValue
        :value="enrollment.data.financeData.representative.cpf"
        title="CPF" />
      <KeyValue
        :value="getOptionFinance2('relationships', 'relationshipId', 'representative')"
        title="Relacionamento com o aluno" />
      <KeyValue
        :value="enrollment.data.financeData.representative.email"
        title="E-mail" />
      <KeyValue
        :value="enrollment.data.financeData.representative.phoneNumber"
        title="Telefone" />
      <KeyValue
        :value="enrollment.data.financeData.representative.landline"
        title="Telefone Fixo" />
      <KeyValue
        :value="enrollment.data.financeData.representative.zipcode"
        title="CEP" />
      <KeyValue
        :value="enrollment.data.financeData.representative.addressKindId"
        title="Tipo" />
      <KeyValue
        :value="enrollment.data.financeData.representative.streetAddress"
        title="Endereço" />
      <KeyValue
        :value="enrollment.data.financeData.representative.addressNumber"
        title="Número" />
      <KeyValue
        :value="enrollment.data.financeData.representative.complementAddress"
        title="Complemento" />
      <KeyValue
        :value="enrollment.data.financeData.representative.neighborhood"
        title="Bairro" />
      <KeyValue
        :value="getOptionFinance2('cities', 'cityId', 'representative')"
        title="Cidade" />
      <KeyValue
        :value="getOptionFinance2('cities', 'stateId', 'representative')"
        title="Estado" />
    </Fieldset>
    <Fieldset
      v-if="enrollment.data.financeData.guarantors
      && enrollment.data.financeData.guarantors.length"
      title="Fiadores">
      <div
        v-for="(guarantor, index) in enrollment.data.financeData.guarantors"
        :key="index"
        class="col-6">
        <Fieldset :title="`Fiador ${index+1}`">
          <KeyValue
            :value="guarantor.cpf"
            title="CPF" />
          <KeyValue
            :value="guarantor.name"
            title="Nome" />
          <KeyValue
            :value="getOptionFinance3('relationships', 'relationshipId', index)"
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
            :value="getOptionFinance3('cities', 'cityId', index)"
            title="Cidade" />
          <KeyValue
            :value="getOptionFinance3('states', 'stateId', index)"
            title="Estado" />
        </Fieldset>
      </div>
    </Fieldset>
  </div>
</template>

<script>
  export default {
    name: "EnrollmentSummary",
    props: {
      enrollment: {
        type: Object,
        required: true,
      },
    },
    methods: {
      getOption(option, key) {
        const choice = this.enrollment.options[option].find(a =>
          a.id === this.enrollment.data.personalData[key]) || {};
        return choice && choice.name;
      },
      getOptionFinance(option, key, key2 = "name") {
        const choice = this.enrollment.options[option].find(a =>
          a.id === this.enrollment.data.financeData[key]);
        return choice && choice[key2];
      },
      getOptionFinance2(option, key, key2, key3 = "name") {
        const choice = this.enrollment.options[option].find(a =>
          a.id === this.enrollment.data.financeData[key2][key]);
        return choice && choice[key3];
      },
      getOptionFinance3(option, key, index, key3 = "name") {
        const choice = this.enrollment.options[option].find(a =>
          a.id === this.enrollment.data.financeData.guarantors[index][key]);
        return choice && choice[key3];
      },
    },
  };
</script>
