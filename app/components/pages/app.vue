<template>
  <div>
    <h1 class="pl3">CMMG-UI</h1>

    <TabGroup headerClass="justify-end border-bottom">
      <Tab title="Matricula">
        <Stepper class="max-width-3 m-auto">
          <Step title="Seus Dados">
            <Card>
              <Fieldset title="Dados Pessoais">
                <div class="flex gutters">
                  <InputBox label="Nome" class="col-12 sm-col-6" />
                  <InputBox label="CPF" class="col-12 sm-col-3" />
                  <InputBox label="Nascimento" class="col-12 sm-col-3" />
                </div>
                <div class="flex gutters">
                  <InputBox label="Nacionalidade" class="col-12 sm-col-3" />
                  <InputBox label="Naturalidade" class="col-12 sm-col-3" />
                  <InputBox label="Estado Civil" class="col-12 sm-col-3" />
                  <InputBox label="Sexo" />
                </div>
              </Fieldset>
              <Fieldset title="Dados de Contato">
                <div class="flex gutters">
                  <InputBox label="E-mail" class="col-12 sm-col-6" />
                  <InputBox label="Telefone" class="col-12 sm-col-6" />
                </div>
              </Fieldset>
              <Fieldset title="Endereço">
                <div class="flex gutters">
                  <InputBox label="CEP" class="col-12 sm-col-3"
                    mask="#####-###" />
                  <InputBox label="Estado" class="col-12 sm-col-3" />
                  <InputBox label="Cidade" class="col-12 sm-col-3" />
                  <InputBox label="Tipo de Endereço" class="col-12 sm-col-3" />
                </div>
                <div class="flex gutters">
                  <InputBox label="Logradouro" hint="Rua, Avenida, etc."
                    class="col-12 sm-col-6" />
                  <InputBox label="Número" class="col-12 sm-col-3" />
                  <InputBox label="Complemento" class="col-12 sm-col-3" />
                </div>
              </Fieldset>
              <Fieldset title="Dados para o Censo">
                <div class="flex gutters">
                  <InputBox label="Raça" class="col-12 sm-col-3" />
                  <InputBox label="Escola" class="col-12 sm-col-3" />
                  <InputBox label="Nome da Escola" class="col-12 sm-col-6" />
                </div>
              </Fieldset>
              <div class="flex justify-end">
                <Btn label="Enviar para análise" class="btn-primary" />
              </div>
            </Card>
          </Step>
          <Step title="Aprovação da Secretaria" />
          <Step title="Dados Financeiros" />
          <Step title="Aprovação do Financeiro" />
          <Step title="Matrícula Concluída!" />
        </Stepper>
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
        </Fieldset>
        <Fieldset title="Colors" class="col-6 pr2">
          <div class="inline-block m1 p4 circle bg-blue"></div>
          <div class="inline-block m1 p4 circle bg-green"></div>
          <div class="inline-block m1 p4 circle bg-red"></div>
          <div class="inline-block m1 p4 circle bg-yellow"></div>
          <div class="inline-block m1 p4 circle bg-silver"></div>
          <div class="inline-block m1 p4 circle bg-gray"></div>
        </Fieldset>
        <Fieldset title="Button and Selectors" class="col-6 pr2">
          <a href="#">A link</a>
          <Btn label="A Button" />
          <Btn label="Blue" class="btn-primary" />
          <Btn label="Red" class="btn-primary bg-red" />
          <Btn label="Yellow" class="btn-primary bg-yellow" />

          <div class="flex flex-column">
            <TagGroup v-model="tags" :options="options" class="my1" />
            <ButtonGroup v-model="tag" :options="options" class="my1" />
            <Pager v-model="page" :records="150" class="my1" />
          </div>
        </Fieldset>
        <Fieldset title="Inputs" class="col-6 pr2">
          <InputBox value="A value" label="An input" hint="This is an input" />
          <div class="flex flex-column">
            <Toggle v-model="active" label="A toggle" class="my1" />
            <Checkbox v-model="active" label="A checkbox" class="my1" />
            <RadioGroup v-model="tag" :options="options" class="my1" />
          </div>
        </Fieldset>
        <Fieldset title="Containers" class="col-6 pr2">
          <Alert message="This is an alert" />
          <Alert message="This is an error" type="error" />
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
        options: [
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
