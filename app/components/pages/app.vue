<template>
  <div>
    <Header />
    <Stepper class="max-width-3 m-auto">
      <Step title="Seus Dados" description="Preencha seus dados pessoais">
        <Card title="Seus Dados">
          <div slot="title" class="center mb4n mt2">
            <img src="../../assets/img/people.svg"
              class="rounded border4 border shadow2 avatar bg-white">
          </div>
          <Fieldset>
            <div class="flex gutters flex-wrap">
              <InputBox label="Nome" :size="6"
                hint="Seu nome completo" />
              <InputBox label="CPF" :size="3" v-model="item.cpf"
                mask="###.###.###-##" hint="Ex: 000.000.000-00" />
              <InputBox label="Nascimento" :size="3" mask="##/##/####" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox label="Nacionalidade" :size="3"
                hint="Ex: Brasileiro" />
              <InputBox label="Naturalidade" :size="3"
                hint="Ex: Belo Horizonte" />
              <InputBox label="Estado Civil" :size="3" />
              <DropDown label="Sexo" :size="3" :options="options.gender"
                v-model="item.gender" />
            </div>
          </Fieldset>
          <Fieldset title="Dados de Contato">
            <div class="flex gutters flex-wrap">
              <InputBox label="E-mail" :size="6" />
              <InputBox label="Telefone" :size="6" v-model="item.phone"
                mask="(##) ####-#####?" />
            </div>
          </Fieldset>
          <Fieldset title="Endereço">
            <div class="flex gutters flex-wrap">
              <InputBox v-model="item.cep" label="CEP" :size="3"
                mask="#####-###" />
              <DropDown label="Estado" :size="3" :options="options.uf"
                v-model="item.uf" />
              <InputBox label="Cidade" :size="3" />
              <InputBox label="Tipo de Endereço" :size="3" />
            </div>
            <div class="flex gutters flex-wrap">
              <InputBox label="Logradouro" hint="Sua rua, avenida, etc."
                :size="6" />
              <InputBox label="Número" :size="3" />
              <InputBox label="Complemento" :size="3" />
            </div>
          </Fieldset>
          <Fieldset title="Dados para o Censo">
            <div class="flex gutters flex-wrap">
              <InputBox label="Raça" :size="3" />
              <InputBox label="Escola" :size="3" />
              <InputBox label="Nome da Escola" :size="6" />
            </div>
          </Fieldset>
          <div class="flex justify-end">
            <Btn label="Enviar para análise" class="btn-primary" />
          </div>
        </Card>
      </Step>
      <Step title="Documentação" description="Envie seus documentos
        online, sem precisar de filas ou agendamento de horário." />
      <Step title="Dados Financeiros" description="Aqui você insere seus
        dados de pagamento." />
      <Step title="Aprovação da Secretaria" description="A secretaria irá
        analisar seus documentos para aprovar sua matrícula." />
      <Step title="Aprovação do Financeiro" description="O financeiro irá
        analisar sua matrícula para aprovar sua matrícula." />
      <Step title="Agende uma Visita" description="Após a aprovação
        da secretaria e do financeiro, é hora de agendar um horário
        para comparecer na CMMG." />
      <Step title="Matrícula Concluída!" description="Sua matrícula foi
        concluída!" />
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
