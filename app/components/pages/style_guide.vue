<template>
  <div class="flex justify-end flex-wrap m2">
    <Fieldset title="Button and Selectors" class="col-6 pr2">
      <Btn label="A Button" />
      <Btn label="Blue" class="btn-primary" />
      <Btn label="Red" class="btn-primary bg-red" />
      <Btn label="Yellow" class="btn-primary bg-yellow" />
      <TagGroup v-model="tags" :options="optionsb" class="my1" />
      <ButtonGroup v-model="tag" :options="optionsb" class="my1" />
      <Pager v-model="page" :records="150" class="my1" />
      <Toggle v-model="active" label="A toggle" class="my1" />
      <Checkbox v-model="active" label="A checkbox" class="my1" />
      <RadioGroup v-model="tag" :options="optionsb" class="my1" />
      <InputBox label="An input" hint="This is an input" />
      <Btn label="Modal" class="btn-primary" @click="modal = true" />
      <Modal v-if="modal" @hide="modal = false">
        I'm a modal<br>
      </Modal>
      <Btn label="Hover me" class="btn-primary" data-tip="Tooltip!" />
      <Progress :value="50" />
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
      Here's a spinner:
      <Spinner active class="my2">
        Blurred text
      </Spinner>
    </Fieldset>
    <Fieldset title="Other" class="col-6 pr2">
      <TabGroup>
        <Tab title="A tab" class="flex flex-wrap">
          Its content
        </Tab>
        <Tab title="Another Tab">
        </Tab>
      </TabGroup>
      <Accordion title="Accordion">The content</Accordion>
      <Accordion title="Another accordion">The content</Accordion>
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
  </div>
</template>

<script>
  export default {
    name: "style-guide",
  };
</script>
