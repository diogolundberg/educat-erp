<template>
  <div>
    <Stepper
      :value="1"
      header
      class="max-width-4 m-auto">
      <Step
        title="Seus Dados"
        description="Preencha seus dados pessoais">
        <Card title="Seus Dados">
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
                v-model="item.name"
                :size="6"
                label="Nome"
                hint="Seu nome completo" />
              <InputBox
                v-model="item.socialName"
                :size="6"
                label="Nome Social"
                hint="Seu nome social" />
              <InputBox
                v-model="item.cpf"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                v-model="item.birthdate"
                :size="3"
                label="Nascimento"
                mask="##/##/####" />
              <DropDown
                v-model="item.maritalStatus"
                :size="3"
                :options="options.maritalStatus"
                label="Estado Civil" />
              <DropDown
                v-model="item.gender"
                :size="3"
                :options="options.gender"
                label="Sexo" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.nationality"
                :size="3"
                label="Nacionalidade"
                hint="Ex: Brasileiro" />
              <InputBox
                v-model="item.birthCountry"
                :size="3"
                label="País de Origem"
                hint="Ex: Brasil" />
              <DropDown
                v-model="item.birthUF"
                :size="3"
                :options="options.uf"
                label="UF de Nascimento" />
              <InputBox
                v-model="item.birthCity"
                :size="3"
                label="Naturalidade"
                hint="Cidade de Nascimento" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.graduationYear"
                :size="6"
                label="Ano de conclusão do ensino médio" />
              <InputBox
                v-model="item.graduationCountry"
                :size="6"
                label="País de conclusão do ensino médio" />
            </div>
          </Fieldset>
          <Fieldset title="Dados de Contato">
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.email"
                :size="6"
                label="E-mail" />
              <InputBox
                v-model="item.phone"
                :size="6"
                label="Telefone"
                mask="(##) ####-#####?" />
            </div>
          </Fieldset>
          <Fieldset title="Endereço">
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.cep"
                :size="3"
                label="CEP"
                mask="#####-###" />
              <InputBox
                v-model="item.street"
                :size="6"
                label="Logradouro"
                hint="Sua rua, avenida, etc." />
              <InputBox
                v-model="item.number"
                :size="3"
                label="Número" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.complement"
                :size="3"
                label="Bairro" />
              <InputBox
                v-model="item.city"
                :size="3"
                label="Cidade" />
              <DropDown
                v-model="item.uf"
                :size="3"
                :options="options.uf"
                label="Estado" />
              <DropDown
                v-model="item.addressType"
                :size="3"
                :options="options.addressType"
                label="Tipo de Endereço" />
            </div>
          </Fieldset>
          <Fieldset title="Dados para o Censo">
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.race"
                :size="3"
                label="Raça" />
              <InputBox
                v-model="item.school"
                :size="3"
                label="Escola" />
              <InputBox
                v-model="item.mothersName"
                :size="6"
                label="Nome completo da mãe" />
            </div>
            <div class="flex gutters flex-wrap">
              <RadioGroup
                v-model="item.handicaps"
                :options="options.handicaps"
                label="Possui alguma Deficiência, Transtorno Global do
                  Desenvolvimento, ou Habilidades/Superdotação?" />
            </div>
            <div
              v-if="item.handicaps == 'yes'"
              class="flex gutters flex-wrap">
              <h4>Selecione:</h4>
              <CheckGroup
                v-model="item.handicapList"
                :options="options.handicapList" />
            </div>
          </Fieldset>
          <div class="flex justify-end">
            <Btn
              primary
              label="Enviar para análise" />
          </div>
        </Card>
      </Step>
      <Step
        title="Documentação"
        description="Envie seus documentos online, sem precisar de filas ou
          agendamento de horário." />
      <Step
        title="Dados Financeiros"
        description="Aqui você insere seus dados de pagamento." />
      <Step
        title="Aprovação da Secretaria"
        description="A secretaria irá analisar seus documentos para aprovar
          sua matrícula." />
      <Step
        title="Aprovação do Financeiro"
        description="O financeiro irá analisar sua matrícula para aprovar
          sua matrícula." />
      <Step
        title="Agende uma Visita"
        description="Após a aprovação da secretaria e do financeiro, é hora
          de agendar um horário para comparecer na CMMG." />
      <Step
        title="Matrícula Concluída!"
        description="Sua matrícula foi concluída!" />
    </Stepper>
  </div>
</template>

<script>
  export default {
    name: "App",
    data() {
      return {
        item: {
          cep: "",
          gender: null,
          maritalStatus: null,
          uf: null,
          birthUF: null,

          cpf: "",
          phone: "",
          name: "",
          socialName: "",
          birthdate: "",
          nationality: "",
          birthCountry: "",
          birthCity: "",
          graduationYear: "",
          graduationCountry: "",
          email: "",
          city: "",
          addressType: "",
          street: "",
          number: "",
          complement: "",
          race: "",
          school: "",
          mothersName: "",
          handicapList: [],
        },
        options: {
          gender: [
            { id: "M", name: "Masculino" },
            { id: "F", name: "Feminino" },
          ],
          maritalStatus: [
            { id: "single", name: "Solteiro" },
            { id: "married", name: "Casado" },
          ],
          uf: [
            { id: "MG", name: "MG" },
            { id: "SP", name: "SP" },
            { id: "RJ", name: "RJ" },
          ],
          addressType: [
            { id: "house", name: "Casa" },
          ],
          handicaps: [
            { id: "yes", name: "Sim" },
            { id: "no", name: "Não" },
            { id: "unknown", name: "Não disponho da informação" },
          ],
          handicapList: [
            { id: 1, name: "Cegueira" },
            { id: 2, name: "Baixa Visão" },
            { id: 3, name: "Surdez" },
            { id: 4, name: "Surdocegueira" },
            { id: 5, name: "Deficiência Auditiva" },
            { id: 6, name: "Deficiência Física" },
            { id: 7, name: "Autismos" },
            { id: 8, name: "Transtorno Degenrativo de Infância" },
            { id: 9, name: "Altas Habilidades/Superdotação" },
            { id: 10, name: "Síndrome de Rett" },
            { id: 11, name: "Deficiência Intelectual" },
            { id: 12, name: " Sindrome de Aspergers" },
            { id: 13, name: " Deficiência Múltipla" },
          ],
        },
        step: 1,
      };
    },
  };
</script>
