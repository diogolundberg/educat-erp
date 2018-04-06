<template>
  <div>
    <Header @notifications="notifications = !notifications">
      <div class="flex items-center">
        <Icon name="clock" />
        <div class="mx2 px2 border-white-50 border-left inline-block h6">
          Olá, <strong>{{ enrollment.data.personalData.realName }}</strong><br>
          <template v-if="enrollment.data.deadline">
            O seu processo de matrícula se encerra em {{ daysRemaining }} dias.
          </template>
        </div>
      </div>
    </Header>
    <Notifications
      v-if="notifications"
      @click="notifications = false" />

    <Spinner :active="!enrollment.data.deadline">
      <Stepper
        v-model="step"
        header
        class="max-width-4 m-auto">
        <Step
          :complete="enrollment.data.personalData.state === 'valid'"
          :error="enrollment.data.personalData.state === 'invalid'"
          title="Seus Dados"
          description="Preencha seus dados pessoais">
          <Card
            title="Seus Dados"
            closeable
            :complete="enrollment.data.personalData.state === 'valid'"
            :error="enrollment.data.personalData.state === 'invalid'"
            @close="step = 0">
            <div
              slot="title"
              class="center mb4n mt2">
              <img
                src="../../assets/img/people.svg"
                class="rounded border4 border shadow2 x6 y6 bg-white">
            </div>
            <Fieldset>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.personalData.realName"
                  :errors="enrollment.errors.personalData.RealName"
                  :size="6"
                  required
                  disabled
                  label="Nome"
                  hint="Seu nome completo" />
                <InputBox
                  v-model="enrollment.data.personalData.assumedName"
                  :errors="enrollment.errors.personalData.AssumedName"
                  :size="6"
                  label="Nome Social"
                  hint="Seu nome social" />
                <InputBox
                  v-model="enrollment.data.personalData.cpf"
                  :errors="enrollment.errors.personalData.Cpf"
                  :size="3"
                  :min-size="14"
                  required
                  disabled
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="enrollment.data.personalData.birthDate"
                  :errors="enrollment.errors.personalData.BirthDate"
                  :size="3"
                  :min-size="10"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Nascimento"
                  mask="##/##/####"
                  hint="Ex: 18/12/2001" />
                <DropDown
                  v-model="enrollment.data.personalData.maritalStatusId"
                  :errors="enrollment.errors.personalData.MaritalStatusId"
                  :size="3"
                  :options="enrollment.options.maritalStatuses"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Estado Civil" />
                <DropDown
                  v-model="enrollment.data.personalData.genderId"
                  :errors="enrollment.errors.personalData.GenderId"
                  :size="3"
                  :options="enrollment.options.genders"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Sexo" />
              </div>
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.personalData.nationalityId"
                  :errors="enrollment.errors.personalData.nationalityId"
                  :size="3"
                  :options="enrollment.options.nationalities"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Nacionalidade" />
                <DropDown
                  v-model="enrollment.data.personalData.birthCountryId"
                  :errors="enrollment.errors.personalData.BirthCountryId"
                  :size="3"
                  :options="enrollment.options.countries"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="País de Origem"
                  hint="Ex: Brasil" />
                <DropDown
                  v-model="enrollment.data.personalData.birthStateId"
                  :errors="enrollment.errors.personalData.BirthStateId"
                  :size="3"
                  :options="enrollment.options.states"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="UF de Nascimento" />
                <DropDown
                  v-model="enrollment.data.personalData.birthCityId"
                  :errors="enrollment.errors.personalData.BirthCityId"
                  :size="3"
                  :options="enrollment.options.cities"
                  :filter="enrollment.data.personalData.birthStateId"
                  :disabled="!!enrollment.data.sentAt"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Naturalidade"
                  hint="Cidade de Nascimento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.personalData.highSchoolGraduationYear"
                  :errors="enrollment.errors.personalData.HighSchoolGraduationYear"
                  :size="6"
                  :min-size="4"
                  :max-size="4"
                  :disabled="!!enrollment.data.sentAt"
                  mask="####"
                  required
                  label="Ano de conclusão do ensino médio"
                  hint="Ex: 2017" />
                <DropDown
                  v-model="enrollment.data.personalData.highSchoolGraduationCountryId"
                  :errors="enrollment.errors.personalData.HighSchoolGraduationCountryId"
                  :size="6"
                  :options="enrollment.options.countries"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="País de conclusão do ensino médio" />
              </div>
            </Fieldset>
            <Fieldset title="Dados de Contato">
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.personalData.email"
                  :errors="enrollment.errors.personalData.Email"
                  :size="4"
                  :min-size="6"
                  :max-size="50"
                  email
                  required
                  disabled
                  label="E-mail" />
                <InputBox
                  v-model="enrollment.data.personalData.phoneNumber"
                  :errors="enrollment.errors.personalData.PhoneNumber"
                  :size="4"
                  :min-size="13"
                  :max-size="14"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Telefone"
                  mask="(##) #########?"
                  hint="Ex: (31) 999999999" />
                <InputBox
                  v-model="enrollment.data.personalData.landline"
                  :errors="enrollment.errors.personalData.landline"
                  :size="4"
                  :min-size="13"
                  :max-size="14"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Telefone Fixo"
                  mask="(##) #########?"
                  hint="Ex: (31) 322222222" />
              </div>
            </Fieldset>
            <Fieldset title="Endereço">
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.personalData.zipcode"
                  :errors="enrollment.errors.personalData.Zipcode"
                  :size="3"
                  :min-size="9"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="CEP"
                  mask="#####-###"
                  hint="Ex: 30100-000" />
                <DropDown
                  v-model="enrollment.data.personalData.addressKindId"
                  :errors="enrollment.errors.personalData.AddressKindId"
                  :size="3"
                  :options="enrollment.options.addressKinds"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Tipo de Endereço" />
                <InputBox
                  v-model="enrollment.data.personalData.streetAddress"
                  :errors="enrollment.errors.personalData.StreetAddress"
                  :size="6"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Logradouro"
                  hint="Sua rua, avenida, etc." />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.personalData.complementAddress"
                  :errors="enrollment.errors.personalData.ComplementAddress"
                  :size="3"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Complemento" />
                <InputBox
                  v-model="enrollment.data.personalData.neighborhood"
                  :errors="enrollment.errors.personalData.Neighborhood"
                  :size="3"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Bairro" />
                <DropDown
                  v-model="enrollment.data.personalData.stateId"
                  :errors="enrollment.errors.personalData.StateId"
                  :size="3"
                  :options="enrollment.options.states"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Estado" />
                <DropDown
                  v-model="enrollment.data.personalData.cityId"
                  :errors="enrollment.errors.personalData.cityId"
                  :size="3"
                  :options="enrollment.options.cities"
                  :filter="enrollment.data.personalData.stateId"
                  :disabled="!!enrollment.data.sentAt"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Cidade"
                  hint="Cidade onde Mora" />
              </div>
            </Fieldset>
            <Fieldset title="Dados para o Censo">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.personalData.raceId"
                  :errors="enrollment.errors.personalData.RaceId"
                  :size="3"
                  :options="enrollment.options.races"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Raça" />
                <DropDown
                  v-model="enrollment.data.personalData.highSchoolKindId"
                  :errors="enrollment.errors.personalData.HighSchoolKindId"
                  :size="3"
                  :options="enrollment.options.highSchoolKinds"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Escola" />
                <InputBox
                  v-model="enrollment.data.personalData.mothersName"
                  :errors="enrollment.errors.personalData.MothersName"
                  :size="6"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Nome completo da mãe" />
              </div>
            </Fieldset>
            <Fieldset title="Outras Informações">
              <div>
                <RadioGroup
                  v-model="enrollment.data.personalData.handicap"
                  :errors="enrollment.errors.personalData.Handicap"
                  :options="handicap"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Possui alguma Deficiência, Transtorno Global do
                    Desenvolvimento, ou Habilidades/Superdotação?" />
              </div>
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <div
                class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="enrollment.data.personalData.disabilities"
                  :errors="enrollment.errors.personalData.Disabilities"
                  :options="enrollment.options.disabilities"
                  :disabled="!!enrollment.data.sentAt" />
              </div>
            </Fieldset>
            <Fieldset
              v-if="enrollment.data.personalData.handicap == 'yes'"
              title="Necessidades Especiais">
              <div class="flex gutters flex-wrap">
                <h4>Selecione:</h4>
                <CheckGroup
                  v-model="enrollment.data.personalData.specialNeeds"
                  :errors="enrollment.errors.personalData.SpecialNeeds"
                  :options="enrollment.options.specialNeeds"
                  :disabled="!!enrollment.data.sentAt"
                  :filter="enrollment.data.personalData.disabilities"
                  filter-key="disabilityId" />
              </div>
            </Fieldset>
            <Fieldset title="Documentos">
              <Documents
                v-model="enrollment.data.personalData.documents"
                :types="enrollment.options.personalDocuments"
                :prefix="`onboarding/enrollment/${ id }/personalData/`"
                :disabled="!!enrollment.data.sentAt" />
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                :disabled="!!enrollment.data.sentAt"
                primary
                label="Próximo"
                @click="savePersonalData" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="enrollment.data.financeData.state == 'valid'"
          :error="enrollment.data.financeData.state == 'invalid'"
          title="Dados Financeiros"
          description="Aqui você insere seus dados de pagamento.">
          <Card
            :complete="enrollment.data.financeData.state === 'valid'"
            :error="enrollment.data.financeData.state === 'invalid'"
            title="Dados Financeiros">
            <Fieldset title="Dados Financeiros">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.financeData.paymentMethodId"
                  :errors="enrollment.errors.financeData.paymentMethodId"
                  :size="6"
                  :options="enrollment.options.paymentMethod"
                  :disabled="!!enrollment.data.sentAt"
                  label="Meio de Pagamento" />
                <DropDown
                  v-model="enrollment.data.financeData.planId"
                  :errors="enrollment.errors.financeData.planId"
                  :size="6"
                  :options="enrollment.options.plans"
                  :disabled="!!enrollment.data.sentAt"
                  label="Meio de Pagamento" />
              </div>
            </Fieldset>
            <Fieldset title="Responsável Financeiro">
              <div class="flex gutters flex-wrap">
                <DropDown
                  v-model="enrollment.data.financeData.representative.discriminator"
                  :errors="enrollment.errors.financeData.representative.discriminator"
                  :size="4"
                  :options="discriminators"
                  :disabled="!!enrollment.data.sentAt"
                  label="CPF ou CNPJ" />
                <InputBox
                  v-if="enrollment.data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Documento" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.cpf"
                  :errors="enrollment.errors.financeData.representative.cpf"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="4"
                  :min-size="14"
                  :disabled="!!enrollment.data.sentAt"
                  cpf
                  label="CPF"
                  mask="###.###.###-##"
                  hint="Ex: 000.000.000-00" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.cnpj"
                  :errors="enrollment.errors.financeData.representative.cnpj"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="4"
                  :min-size="18"
                  :disabled="!!enrollment.data.sentAt"
                  cnpj
                  label="CNPJ"
                  mask="##.###.###/####-##"
                  hint="Ex: 00.000.000/0000-00" />
                <InputBox
                  v-if="enrollment.data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Nome" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.name"
                  :errors="enrollment.errors.financeData.representative.name"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Nome completo" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.name"
                  :errors="enrollment.errors.financeData.representative.name"
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Razão Social" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-if="enrollment.data.financeData.representative.discriminator == null"
                  :size="4"
                  disabled
                  label="Contato" />
                <InputBox
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativeCompany'"
                  v-model="enrollment.data.financeData.representative.contact"
                  :errors="enrollment.errors.financeData.representative.contact"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Pessoa de Contato" />
                <DropDown
                  v-if="enrollment.data.financeData.representative.discriminator
                    == 'RepresentativePerson'"
                  v-model="enrollment.data.financeData.representative.relationshipId"
                  :errors="enrollment.errors.financeData.representative.relationshipId"
                  :size="4"
                  :options="enrollment.options.relationships"
                  :disabled="!!enrollment.data.sentAt"
                  label="Relacionamento com o aluno" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.streetAddress"
                  :errors="enrollment.errors.financeData.representative.streetAddress"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Logradouro" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.complementAddress"
                  :errors="enrollment.errors.financeData.representative.complementAddress"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Complemento" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.financeData.representative.neighborhood"
                  :errors="enrollment.errors.financeData.representative.neighborhood"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Bairro" />
                <DropDown
                  v-model="enrollment.data.financeData.representative.stateId"
                  :errors="enrollment.errors.financeData.representative.stateId"
                  :size="4"
                  :options="enrollment.options.states"
                  :disabled="!!enrollment.data.sentAt"
                  required
                  label="Estado" />
                <DropDown
                  v-model="enrollment.data.financeData.representative.cityId"
                  :errors="enrollment.errors.financeData.representative.cityId"
                  :size="4"
                  :options="enrollment.options.cities"
                  :filter="enrollment.data.financeData.representative.stateId"
                  :disabled="!!enrollment.data.sentAt"
                  filter-key="stateId"
                  key-id="name"
                  required
                  label="Cidade" />
              </div>
              <div class="flex gutters flex-wrap">
                <InputBox
                  v-model="enrollment.data.financeData.representative.landline"
                  :errors="enrollment.errors.financeData.representative.landline"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Telefone"
                  mask="(##) ####-####" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.phoneNumber"
                  :errors="enrollment.errors.financeData.representative.phoneNumber"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="Celular"
                  mask="(##) #####-####" />
                <InputBox
                  v-model="enrollment.data.financeData.representative.email"
                  :errors="enrollment.errors.financeData.representative.email"
                  :size="4"
                  :disabled="!!enrollment.data.sentAt"
                  label="E-mail" />
             </div>
            </Fieldset>
            <Fieldset
              title="Fiadores">
              <Multi
                v-model="enrollment.data.financeData.guarantors"
                :errors="enrollment.errors.financeData"
                :default="emptyGuarantor"
                :disabled="!!enrollment.data.sentAt"
                error-key="guarantors">
                <template slot-scope="{ item, error }">
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.cpf"
                      :errors="error.cpf"
                      :size="4"
                      :min-size="14"
                      :disabled="!!enrollment.data.sentAt"
                      cpf
                      label="CPF"
                      mask="###.###.###-##"
                      hint="Ex: 000.000.000-00" />
                    <InputBox
                      v-model="item.name"
                      :errors="error.name"
                      :size="8"
                      :disabled="!!enrollment.data.sentAt"
                      label="Nome" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.relationship"
                      :errors="error.relationship"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Relacionamento" />
                    <InputBox
                      v-model="item.streetAddress"
                      :errors="error.streetAddress"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Endereço Completo" />
                    <InputBox
                      v-model="item.complementAddress"
                      :errors="error.complementAddress"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Complemento" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.neighborhood"
                      :errors="error.neighborhood"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Bairro" />
                    <DropDown
                      v-model="item.stateId"
                      :errors="error.stateId"
                      :size="4"
                      :options="enrollment.options.states"
                      :disabled="!!enrollment.data.sentAt"
                      required
                      label="Estado" />
                    <DropDown
                      v-model="item.cityId"
                      :errors="error.cityId"
                      :size="4"
                      :options="enrollment.options.cities"
                      :filter="item.stateId"
                      :disabled="!!enrollment.data.sentAt"
                      filter-key="stateId"
                      key-id="name"
                      required
                      label="Cidade"
                      hint="Cidade onde Mora" />
                  </div>
                  <div class="flex gutters flex-wrap">
                    <InputBox
                      v-model="item.landline"
                      :errors="error.landline"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Telefone"
                      mask="(##) ####-####" />
                    <InputBox
                      v-model="item.phoneNumber"
                      :errors="error.phoneNumber"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="Celular"
                      mask="(##) #####-####" />
                    <InputBox
                      v-model="item.email"
                      :errors="error.email"
                      :size="4"
                      :disabled="!!enrollment.data.sentAt"
                      label="E-mail" />
                  </div>
                  <Documents
                    v-model="item.documents"
                    :errors="error.documents"
                    :types="enrollment.options.guarantorDocuments"
                    :prefix="`onboarding/enrollment/${ id }/financeData/`"
                    :disabled="!!enrollment.data.sentAt" />
                </template>
              </Multi>
            </Fieldset>
            <div class="flex justify-end">
              <Btn
                primary
                label="Próximo"
                :disabled="!!enrollment.data.sentAt"
                @click="saveFinanceData" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="!enrollment.data.sentAt"
          title="Enviar para Análise"
          description="Enviar para aprovação da secretaria e departamento
            financeiro">
          <Card
            closeable
            title="Enviar para Análise">
            <BaseErrors v-model="enrollment.messages" />
            <p>Envie seus dados para a secetaria e para o
              departamento financeiro para aprovação.</p>
            <div class="center">
              <img
                :style="{ 'max-width': '8rem' }"
                src="../../assets/img/card.svg">
            </div>
            <div class="flex justify-end">
              <Btn
                primary
                label="Enviar"
                @click="submitEnrollment" />
            </div>
          </Card>
        </Step>
        <Step
          v-if="enrollment.data.sentAt"
          complete
          title="Aguardando Aprovação"
          description="A secretaria e o departamento financeiro estão analisando
            seus documentos">
          <Card title="Dados enviados">
            <BaseErrors
              success
              v-model="enrollment.messages" />
            <p>Seus dados foram enviados. Agora a secretaria e o departamento
              financeiro estão analisando seus documentos.</p>
            <div class="center">
              <Animation
                name="success" />
            </div>
          </Card>
        </Step>
        <Step
          :complete="!!enrollment.data.academicApproval"
          title="Aprovação da Secretaria"
          description="A secretaria irá analisar seus documentos para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.academicApproval"
            title="Aprovação da Secretaria">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval"
            title="Matrícula Aprovada pela Secretaria">
            A secretaria já aprovou sua matrícula.
          </Card>
        </Step>
        <Step
          :complete="!!enrollment.data.financeApproval"
          title="Aprovação do Financeiro"
          description="O financeiro irá analisar sua matrícula para aprovar
            sua matrícula.">
          <Card
            v-if="!enrollment.data.academicApproval"
            title="Aprovação do Financeiro">
            Sua aprovação ainda está pendente.
          </Card>
          <Card
            v-if="enrollment.data.academicApproval"
            title="Matrícula Aprovada pelo Financeiro">
            Nosso departamento financeiro já aprovou sua matrícula.
          </Card>
        </Step>
        <Step
          title="Agende uma Visita"
          description="Após a aprovação da secretaria e do financeiro, é hora
            de agendar um horário para comparecer na CMMG." />
        <Step
          title="Matrícula Concluída!"
          description="Sua matrícula foi concluída!" />
      </Stepper>
    </Spinner>
  </div>
</template>

<script>
  import { debounce } from "lodash";

  export default {
    name: "Enrollment",
    props: {
      id: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        step: 1,
        notifications: false,
        handicap: [
          { id: "yes", name: "Sim" },
          { id: "no", name: "Não" },
          { id: "unknown", name: "Não disponho da informação" },
        ],
        discriminators: [
          { id: "RepresentativePerson", name: "CPF" },
          { id: "RepresentativeCompany", name: "CNPJ" },
        ],
        emptyGuarantor: {
          discriminator: "RepresentativePerson",
          cpf: "",
          cnpj: "",
          name: "",
          contact: "",
          relationship: "",
          streetAddress: "",
          complementAddress: "",
          neighborhood: "",
          phoneNumber: "",
          landline: "",
          email: "",
          cityId: null,
          stateId: null,
        },
      };
    },
    computed: {
      enrollment() {
        return this.$store.state.enrollment;
      },
      daysRemaining() {
        const day = 1000 * 60 * 60 * 24;
        const remaining = new Date(this.enrollment.data.deadline) - new Date();
        return Math.floor(remaining / day);
      },
    },
    watch: {
      "enrollment.data.personalData.zipcode": debounce(function findZipcode() {
        this.$store.dispatch("findZipcode");
      }, 500),
    },
    async mounted() {
      try {
        await this.$store.dispatch("getEnrollment", this.id);
        this.goToStep();
      } catch (ex) {
        this.$router.push("/404");
      }
    },
    methods: {
      goToStep() {
        if (this.enrollment.data.personalData.state === "valid") {
          this.step = 2;
        }
        if (this.enrollment.data.financeData.state === "valid") {
          this.step = 3;
        }
        if (this.enrollment.data.sentAt) {
          this.step = 3;
        }
      },
      async savePersonalData() {
        const token = this.id;
        const data = this.enrollment.data.personalData;
        await this.$store.dispatch("setPersonalData", { token, data });
        this.goToStep();
      },
      async saveFinanceData() {
        const token = this.id;
        const data = this.enrollment.data.financeData;
        await this.$store.dispatch("setFinanceData", { token, data });
        this.goToStep();
      },
      async submitEnrollment() {
        const token = this.id;
        await this.$store.dispatch("submitEnrollment", { token });
      },
    },
  };
</script>
