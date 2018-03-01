<template>
  <div>
    <Header />
    <Stepper class="max-width-3 m-auto">
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
                :size="6"
                label="Nome"
                hint="Seu nome completo" />
              <InputBox
                v-model="item.cpf"
                :size="3"
                label="CPF"
                mask="###.###.###-##"
                hint="Ex: 000.000.000-00" />
              <InputBox
                :size="3"
                label="Nascimento"
                mask="##/##/####" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                :size="3"
                label="Nacionalidade"
                hint="Ex: Brasileiro" />
              <InputBox
                :size="3"
                label="Naturalidade"
                hint="Ex: Belo Horizonte" />
              <InputBox
                :size="3"
                label="Estado Civil" />
              <DropDown
                v-model="item.gender"
                :size="3"
                :options="options.gender"
                label="Sexo" />
            </div>
          </Fieldset>
          <Fieldset title="Dados de Contato">
            <div class="flex gutters flex-wrap">
              <InputBox
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
                :size="3"
                label="Cidade" />
              <InputBox
                :size="3"
                label="Tipo de Endereço" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox
                :size="6"
                label="Logradouro"
                hint="Sua rua, avenida, etc." />
              <InputBox
                :size="3"
                label="Número" />
              <InputBox
                :size="3"
                label="Complemento" />
            </div>
          </Fieldset>
          <Fieldset title="Dados para o Censo">
            <div class="flex gutters flex-wrap">
              <InputBox
                :size="3"
                label="Raça" />
              <InputBox
                :size="3"
                label="Escola" />
              <InputBox
                :size="6"
                label="Nome da Escola" />
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
          uf: null,
        },
        options: {
          gender: [
            { id: "M", name: "Masculino" },
            { id: "F", name: "Feminino" },
          ],
          uf: [
            { id: "MG", name: "MG" },
            { id: "SP", name: "SP" },
            { id: "RJ", name: "RJ" },
          ],
        },
        step: 1,
      };
    },
  };
</script>
