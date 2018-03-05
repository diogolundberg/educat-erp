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
              <DropDown
                v-model="item.uf"
                :size="3"
                :options="options.uf"
                label="Estado" />
              <InputBox
                v-model="item.city"
                :size="3"
                label="Cidade" />
              <DropDown
                v-model="item.addressType"
                :size="3"
                :options="options.addressType"
                label="Tipo de Endereço" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                v-model="item.street"
                :size="6"
                label="Logradouro"
                hint="Sua rua, avenida, etc." />
              <InputBox
                v-model="item.number"
                :size="3"
                label="Número" />
              <InputBox
                v-model="item.complement"
                :size="3"
                label="Complemento" />
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
          </Fieldset>
          <div class="flex justify-end">
            <Btn
                label="Enviar para análise"
                class="btn-primary" />
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
          cep: null,
          gender: null,
          cpf: null,
          phone: null,
          maritalStatus: null,
          uf: null,
          name: null,
          socialName: null,
          birthdate: null,
          nationality: null,
          birthCountry: null,
          birthUF: null,
          birthCity: null,
          graduationYear: null,
          graduationCountry: null,
          email: null,
          city: null,
          addressType: null,
          street: null,
          number: null,
          complement: null,
          race: null,
          school: null,
          mothersName: null,
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
        },
        step: 1,
      };
    },
  };
</script>
