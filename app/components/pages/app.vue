<template>
  <div>
    <Header />
    <Stepper class="max-width-3 m-auto">
      <Step title="Seus Dados" description="Preencha seus dados pessoais">
        <Card title="Seus Dados">
          <div slot="title" class="center mb4n mt2">
            <img src="../../assets/img/people.svg"
              class="rounded border4 border shadow1 avatar bg-white">
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
              <InputBox label="Telefone" :size="6"
                mask="(##) ####-#####?" />
            </div>
          </Fieldset>
          <Fieldset title="Endereço">
            <div class="flex gutters flex-wrap">
              <InputBox label="CEP" :size="3"
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

    <TabGroup header-class="justify-end border-bottom">
      <Tab title="-">
      </Tab>
      <Tab title="Style Guide" class="flex flex-wrap">
        <Fieldset title="Typography" class="col-12 sm-col-6 pr2">
          <h1>Heading Level One</h1>
          <h2>Heading Level Two</h2>
          <h3>Heading Level Three</h3>
          <h4>Heading Level Four</h4>
          <h5>Heading Level Five</h5>
          <h6>Heading Level Six</h6>
          <p>Normal Paragraph</p>
          <Badge>Badge</Badge><Badge>2</Badge>
        </Fieldset>
        <Fieldset title="Colors" class="col-6 pr2">
          <div class="inline-block m1 p4 circle bg-blue" />
          <div class="inline-block m1 p4 circle bg-green" />
          <div class="inline-block m1 p4 circle bg-red" />
          <div class="inline-block m1 p4 circle bg-yellow" />
          <div class="inline-block m1 p4 circle bg-silver" />
          <div class="inline-block m1 p4 circle bg-gray" />
        </Fieldset>
        <Fieldset title="Button and Selectors" class="col-6 pr2">
          <a href="#">A link</a>
          <Btn label="A Button" />
          <Btn label="Blue" class="btn-primary" />
          <Btn label="Red" class="btn-primary bg-red" />
          <Btn label="Yellow" class="btn-primary bg-yellow" />

          <div class="flex flex-column">
            <TagGroup v-model="tags" :options="optionsb" class="my1" />
            <ButtonGroup v-model="tag" :options="optionsb" class="my1" />
            <Pager v-model="page" :records="150" class="my1" />
          </div>
        </Fieldset>
        <Fieldset title="Inputs" class="col-6 pr2">
          <InputBox value="A value" label="An input" hint="This is an input" />
          <div class="flex flex-column">
            <Toggle v-model="active" label="A toggle" class="my1" />
            <Checkbox v-model="active" label="A checkbox" class="my1" />
            <RadioGroup v-model="tag" :options="optionsb" class="my1" />
          </div>
        </Fieldset>
        <Fieldset title="Containers" class="col-6 pr2">
          <Alert success>
            This is a message
          </Alert>
          <Alert warning>
            This is a warning
          </Alert>
          <Alert error>
            This is an error
          </Alert>
          <Alert info>
            This is an info message <Badge warning>And a badge</Badge>
          </Alert>
          <Card title="Card">
            This is a card
          </Card>
          <Accordion title="Accordion">The content</Accordion>
          <Accordion title="Another accordion">The content</Accordion>
          Here's a spinner:
          <Spinner active class="my2">
            Blurred text
          </Spinner>
        </Fieldset>
        <Fieldset title="Steppers" class="col-6 pr2">
          <Card>
            <Stepper v-model="step">
              <Step title="Step 1" description="My description">
                <InputBox label="Name" value="" />
                <Btn label="Continue" class="btn-primary" @click="step = 2" />
              </Step>
              <Step title="Step 2" description="My description">
                <InputBox label="E-mail" value="" />
                <Btn label="Continue" class="btn-primary" @click="step = 3" />
              </Step>
              <Step title="Step 3" description="My description">
                <h3>You're done!</h3>
                <Btn label="Reset" class="btn-primary" @click="step = 1" />
              </Step>
            </Stepper>
          </Card>
        </Fieldset>
        <Fieldset title="Special" class="col-6 pr2">
          <Btn label="Modal" class="btn-primary" @click="modal = true" />
          <Modal v-if="modal" @hide="modal = false">
            I'm a modal<br>
          </Modal>
          <Btn label="Hover me" class="btn-primary" data-tip="Tooltip!" />
          <Progress :value="50" />
        </Fieldset>
      </Tab>
    </TabGroup>
  </div>
</template>

<script>
  export default {
    name: "App",
    data() {
      return {
        item: {
          gender: null,
          cpf: null,
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
        optionsb: [
          { id: 1, name: "One" },
          { id: 2, name: "Two" },
        ],
        tags: [],
        tag: null,
        page: 1,
        modal: false,
        text: "",
        active: false,
        step: 1,
        cep: null,
      };
    },
  };
</script>
