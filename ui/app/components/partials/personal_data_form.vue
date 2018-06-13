<template>
  <div>
    <DataForm
      :id="id"
      :endpoint="baseUrl"
      :sub-key="['data']"
      post
      @success="$emit('success')">
      <template slot-scope="{ item, errors }">
        <div class="center mt2">
          <UploadButton
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
          </UploadButton>
        </div>
        <Fieldset id="personal">
          <InputBox
            v-model="item.realName"
            :errors="errors.realName"
            :size="6"
            required
            disabled
            label="Nome"
            hint="Seu nome completo" />
          <InputBox
            v-model="item.assumedName"
            :errors="errors.assumedName"
            :disabled="disabled"
            :size="6"
            label="Nome Social"
            hint="Seu nome social" />
          <InputBox
            v-model="item.cpf"
            :errors="errors.cpf"
            :size="3"
            :min-size="14"
            required
            disabled
            label="CPF"
            mask="###.###.###-##"
            hint="Ex: 000.000.000-00" />
          <InputBox
            v-model="item.birthDate"
            :errors="errors.birthDate"
            :size="3"
            :min-size="10"
            :disabled="disabled"
            date
            required
            label="Nascimento"
            mask="##/##/####"
            hint="Ex: 18/12/2001" />
          <DropDown
            v-model="item.maritalStatusId"
            :errors="errors.maritalStatusId"
            :size="3"
            :options="options.maritalStatuses"
            :disabled="disabled"
            required
            label="Estado Civil" />
          <DropDown
            v-model="item.genderId"
            :errors="errors.genderId"
            :size="3"
            :options="options.genders"
            :disabled="disabled"
            required
            label="Sexo" />
          <DropDown
            v-model="item.nationalityId"
            :errors="errors.nationalityId"
            :size="3"
            :options="options.nationalities"
            :disabled="disabled"
            required
            label="Nacionalidade" />
          <DropDown
            v-model="item.birthCountryId"
            :errors="errors.birthCountryId"
            :size="3"
            :options="options.countries"
            :disabled="disabled"
            required
            label="País de Origem"
            hint="Ex: Brasil" />
          <template v-if="hasUF(item.birthCountryId)">
            <DropDown
              v-model="item.birthStateId"
              :errors="errors.birthStateId"
              :size="3"
              :options="options.states"
              :disabled="disabled"
              required
              label="UF de Nascimento" />
            <DropDown
              v-model="item.birthCityId"
              :errors="errors.birthCityId"
              :size="3"
              :options="options.cities"
              :filter="item.birthStateId"
              :disabled="disabled"
              filter-key="stateId"
              key-id="name"
              required
              label="Naturalidade"
              hint="Cidade de Nascimento" />
          </template>
          <template v-else>
            <InputBox
              v-model="item.birthStateName"
              :errors="errors.birthStateName"
              :size="3"
              :disabled="disabled"
              required
              label="UF de Nascimento" />
            <InputBox
              v-model="item.birthCityName"
              :errors="errors.birthCityName"
              :size="3"
              :disabled="disabled"
              required
              label="Naturalidade"
              hint="Cidade de Nascimento" />
          </template>
          <InputBox
            v-model="item.highSchoolGraduationYear"
            :errors="errors.highSchoolGraduationYear"
            :size="6"
            :min-size="4"
            :max-size="4"
            :disabled="disabled"
            :max-value="new Date().getFullYear()"
            mask="####"
            required
            label="Ano de conclusão do ensino médio"
            hint="Ex: 2017" />
          <DropDown
            v-model="item.highSchoolGraduationCountryId"
            :errors="errors.highSchoolGraduationCountryId"
            :size="6"
            :options="options.countries"
            :disabled="disabled"
            required
            label="País de conclusão do ensino médio" />
        </Fieldset>
        <ContactBlock
          id="contact"
          v-model="item"
          :errors="errors"
          :disabled="disabled"
          disable-email />
        <AddressBlock
          id="address"
          v-model="item"
          :errors="errors"
          :disabled="disabled"
          :options="options" />
        <Fieldset
          id="census"
          title="Dados para o Censo">
          <DropDown
            v-model="item.raceId"
            :errors="errors.raceId"
            :size="3"
            :options="options.races"
            :disabled="disabled"
            required
            label="Raça" />
          <DropDown
            v-model="item.highSchoolKindId"
            :errors="errors.highSchoolKindId"
            :size="3"
            :options="options.highSchoolKinds"
            :disabled="disabled"
            required
            label="Escola" />
          <InputBox
            v-model="item.mothersName"
            :errors="errors.mothersName"
            :size="6"
            :disabled="disabled"
            required
            label="Nome completo da mãe" />
        </Fieldset>
        <Fieldset
          id="other"
          title="Outras Informações">
          <RadioGroup
            v-model="item.handicap"
            :errors="errors.handicap"
            :options="handicap"
            :disabled="disabled"
            required
            label="Possui alguma Deficiência, Transtorno Global do
              Desenvolvimento, ou Habilidades/Superdotação?" />
        </Fieldset>
        <Fieldset
          v-if="item.handicap == 'yes'"
          id="specialNeeds"
          title="Selecione">
          <CheckGroup
            v-model="item.disabilities"
            :errors="errors.disabilities"
            :options="options.disabilities"
            :disabled="disabled" />
        </Fieldset>
        <Fieldset
          v-if="item.handicap == 'yes'"
          title="Necessidades Especiais">
          <CheckGroup
            v-model="item.specialNeeds"
            :errors="errors.specialNeeds"
            :options="options.specialNeeds"
            :disabled="disabled"
            :filter="item.disabilities"
            filter-key="disabilityId" />
        </Fieldset>
        <Fieldset
          id="documents"
          title="Documentos">
          <Documents
            v-model="item.documents"
            :types="options.personalDocuments"
            :errors="errors.documents"
            :prefix="`onboarding/enrollment/${ id }/personalData/`"
            :disabled="disabled"
            :validations="validationsFor(item)" />
        </Fieldset>
      </template>
    </DataForm>
  </div>
</template>

<script>
  export default {
    name: "PersonalDataForm",
    props: {
      id: {
        type: [Number, String],
        required: true,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      options: {
        type: Object,
        default: () => ({}),
      },
      onboardingYear: {
        type: String,
        required: true,
      },
    },
    computed: {
      handicap() {
        return [
          { id: "yes", name: "Sim" },
          { id: "no", name: "Não" },
          { id: "unknown", name: "Não disponho da informação" },
        ];
      },
      baseUrl() {
        const { onboardingEndpoint } = this.$store.getters;
        return `${onboardingEndpoint}/v2/PersonalDatas`;
      },
    },
    methods: {
      hasUF(birthCountryId) {
        const birthCountry = this.options.countries.find(a =>
          a.id === birthCountryId);
        return birthCountry && birthCountry.hasUF;
      },
      validationsFor(item) {
        const matches = (opt, key, val) =>
          this.options[opt].filter(a => a[key]).map(a => a.id).includes(val);

        const isNative = matches(
          "nationalities",
          "checkNative",
          item.nationalityId,
        );
        const isForeigner = matches(
          "nationalities",
          "checkForeign",
          item.nationalityId,
        );
        const hasDraft = matches(
          "genders",
          "checkMilitaryDraft",
          item.genderId,
        );
        const foreignGraduation = matches(
          "countries",
          "checkForeignGraduation",
          item.highSchoolGraduationCountryId,
        );
        const gradutionCurrentYear = this.onboardingYear ===
          item.highSchoolGraduationYear;

        return [
          "MinorAge",
          isNative && "Native",
          isForeigner && "Foreigner",
          hasDraft && "MilitaryDraft",
          foreignGraduation && "ForeignGraduation",
          gradutionCurrentYear && "GraduationYear",
          !gradutionCurrentYear && "NotGraduationYear",
        ].filter(a => a);
      },
    },
  };
</script>
