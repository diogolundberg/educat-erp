<template>
  <div>
    <BaseErrors
      v-model="messages" />
    <div
      class="center mt2">
      <UploadZone
        :value="photo"
        prefix="picture"
        @input="$emit('photo', $event)">
        <img
          v-if="photo"
          :src="photo"
          class="rounded50 shadow1 x6 y6 bg-white">
        <img
          v-if="!photo"
          src="../../assets/img/people.svg"
          class="rounded50 shadow1 x6 y6 bg-white">
        <a
          v-if="!photo"
          class="block green my2"
          href="#">
          Mudar foto
        </a>
      </UploadZone>
    </div>
    <Fieldset id="personal">
      <InputBox
        v-model="data.realName"
        :errors="errors.realName"
        :size="6"
        required
        disabled
        label="Nome"
        hint="Seu nome completo" />
      <InputBox
        v-model="data.assumedName"
        :errors="errors.assumedName"
        :disabled="!data.editable"
        :size="6"
        label="Nome Social"
        hint="Seu nome social" />
      <InputBox
        v-model="data.cpf"
        :errors="errors.cpf"
        :size="3"
        :min-size="14"
        required
        disabled
        label="CPF"
        mask="###.###.###-##"
        hint="Ex: 000.000.000-00" />
      <InputBox
        v-model="data.birthDate"
        :errors="errors.birthDate"
        :size="3"
        :min-size="10"
        :disabled="!data.editable"
        date
        required
        label="Nascimento"
        mask="##/##/####"
        hint="Ex: 18/12/2001" />
      <DropDown
        v-model="data.maritalStatusId"
        :errors="errors.maritalStatusId"
        :size="3"
        :options="options.maritalStatuses"
        :disabled="!data.editable"
        required
        label="Estado Civil" />
      <DropDown
        v-model="data.genderId"
        :errors="errors.genderId"
        :size="3"
        :options="options.genders"
        :disabled="!data.editable"
        required
        label="Sexo" />
      <DropDown
        v-model="data.nationalityId"
        :errors="errors.nationalityId"
        :size="3"
        :options="options.nationalities"
        :disabled="!data.editable"
        required
        label="Nacionalidade" />
      <DropDown
        v-model="data.birthCountryId"
        :errors="errors.birthCountryId"
        :size="3"
        :options="options.countries"
        :disabled="!data.editable"
        required
        label="País de Origem"
        hint="Ex: Brasil" />
      <template v-if="birthHasUF">
        <DropDown
          v-model="data.birthStateId"
          :errors="errors.birthStateId"
          :size="3"
          :options="options.states"
          :disabled="!data.editable"
          required
          label="UF de Nascimento" />
        <DropDown
          v-model="data.birthCityId"
          :errors="errors.birthCityId"
          :size="3"
          :options="options.cities"
          :filter="data.birthStateId"
          :disabled="!data.editable"
          filter-key="stateId"
          key-id="name"
          required
          label="Naturalidade"
          hint="Cidade de Nascimento" />
      </template>
      <template v-else>
        <InputBox
          v-model="data.birthStateName"
          :errors="errors.birthStateName"
          :size="3"
          :disabled="!data.editable"
          required
          label="UF de Nascimento" />
        <InputBox
          v-model="data.birthCityName"
          :errors="errors.birthCityName"
          :size="3"
          :disabled="!data.editable"
          required
          label="Naturalidade"
          hint="Cidade de Nascimento" />
      </template>
      <InputBox
        v-model="data.highSchoolGraduationYear"
        :errors="errors.highSchoolGraduationYear"
        :size="6"
        :min-size="4"
        :max-size="4"
        :disabled="!data.editable"
        :max-value="new Date().getFullYear()"
        mask="####"
        required
        label="Ano de conclusão do ensino médio"
        hint="Ex: 2017" />
      <DropDown
        v-model="data.highSchoolGraduationCountryId"
        :errors="errors.highSchoolGraduationCountryId"
        :size="6"
        :options="options.countries"
        :disabled="!data.editable"
        required
        label="País de conclusão do ensino médio" />
    </Fieldset>
    <ContactBlock
      id="contact"
      v-model="data"
      :errors="errors"
      :disabled="!data.editable"
      disable-email />
    <AddressBlock
      id="address"
      v-model="data"
      :errors="errors"
      :disabled="!data.editable"
      :options="options" />
    <Fieldset
      id="census"
      title="Dados para o Censo">
      <DropDown
        v-model="data.raceId"
        :errors="errors.raceId"
        :size="3"
        :options="options.races"
        :disabled="!data.editable"
        required
        label="Raça" />
      <DropDown
        v-model="data.highSchoolKindId"
        :errors="errors.highSchoolKindId"
        :size="3"
        :options="options.highSchoolKinds"
        :disabled="!data.editable"
        required
        label="Escola" />
      <InputBox
        v-model="data.mothersName"
        :errors="errors.mothersName"
        :size="6"
        :disabled="!data.editable"
        required
        label="Nome completo da mãe" />
    </Fieldset>
    <Fieldset
      id="other"
      title="Outras Informações">
      <RadioGroup
        v-model="data.handicap"
        :errors="errors.handicap"
        :options="handicap"
        :disabled="!data.editable"
        required
        label="Possui alguma Deficiência, Transtorno Global do
          Desenvolvimento, ou Habilidades/Superdotação?" />
    </Fieldset>
    <Fieldset
      v-if="data.handicap == 'yes'"
      id="specialNeeds"
      title="Selecione">
      <CheckGroup
        v-model="data.disabilities"
        :errors="errors.disabilities"
        :options="options.disabilities"
        :disabled="!data.editable" />
    </Fieldset>
    <Fieldset
      v-if="data.handicap == 'yes'"
      title="Necessidades Especiais">
      <CheckGroup
        v-model="data.specialNeeds"
        :errors="errors.specialNeeds"
        :options="options.specialNeeds"
        :disabled="!data.editable"
        :filter="data.disabilities"
        filter-key="disabilityId" />
    </Fieldset>
    <Fieldset
      id="documents"
      title="Documentos">
      <Documents
        v-model="data.documents"
        :types="options.personalDocuments"
        :errors="errors.documents"
        :prefix="`onboarding/enrollment/${ id }/personalData/`"
        :disabled="!data.editable"
        :validations="validations" />
    </Fieldset>
    <div class="flex justify-end">
      <Btn
        :disabled="!data.editable"
        primary
        label="Próximo"
        @click="$emit('submit')" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "PersonalDataForm",
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
      photo: {
        type: String,
        required: false,
        default: "",
      },
    },
  };
</script>
