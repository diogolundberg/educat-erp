<template>
  <div>
    <h1 class="pl3">CMMG-UI</h1>

    <TabGroup headerClass="justify-end border-bottom">
      <Tab title="Home" class="flex flex-wrap flex-pr2">
        <Fieldset title="Typography" class="col-6">
          <h1>Heading Level One</h1>
          <h2>Heading Level Two</h2>
          <h3>Heading Level Three</h3>
          <h4>Heading Level Four</h4>
          <h5>Heading Level Five</h5>
          <h6>Heading Level Six</h6>
          <p>Normal Paragraph</p>
        </Fieldset>
        <Fieldset title="Colors" class="col-6">
          <div class="inline-block m1 p4 circle bg-blue"></div>
          <div class="inline-block m1 p4 circle bg-green"></div>
          <div class="inline-block m1 p4 circle bg-red"></div>
          <div class="inline-block m1 p4 circle bg-yellow"></div>
          <div class="inline-block m1 p4 circle bg-silver"></div>
          <div class="inline-block m1 p4 circle bg-gray"></div>
        </Fieldset>
        <Fieldset title="Button and Selectors" class="col-6 mr">
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
        <Fieldset title="Inputs" class="col-6">
          <InputBox value="A value" label="An input" hint="This is an input" />
          <div class="flex flex-column">
            <Toggle v-model="active" label="A toggle" class="my1" />
            <Checkbox v-model="active" label="A checkbox" class="my1" />
            <RadioGroup v-model="tag" :options="options" class="my1" />
          </div>
        </Fieldset>
        <Fieldset title="Containers" class="col-6">
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
        <Fieldset title="Steppers" class="col-6">
          <Card>
            <Stepper v-model="step">
              <Step :index="1" title="Step 1" description="My description">
                <InputBox label="Name" value="" />
                <Btn label="Continue" class="btn-primary" @click="step = 2" />
              </Step>
              <Step :index="2" title="Step 2" description="My description">
                <InputBox label="E-mail" value="" />
                <Btn label="Continue" class="btn-primary" @click="step = 3" />
              </Step>
              <Step :index="3" title="Step 3" description="My description">
                <h3>You're done!</h3>
                <Btn label="Reset" class="btn-primary" @click="step = 1" />
              </Step>
            </Stepper>
          </Card>
        </Fieldset>
        <Fieldset title="Special" class="col-6">
          <Btn label="Modal" class="btn-primary" @click="modal = true" />
          <Modal v-if="modal" @hide="modal = false">
            I'm a modal<br>
          </Modal>
          <Btn label="Hover me" class="btn-primary" data-tip="Tooltip!" />
          <Progress :value="50" />
        </Fieldset>
      </Tab>
      <Tab title="Classes" />
      <Tab title="Students" />
      <Tab title="Back Office" />
      <Tab title="Accounts" />
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
      };
    },
  };
</script>
