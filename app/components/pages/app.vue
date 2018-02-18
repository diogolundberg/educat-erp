<template>
  <div>
    <h1 class="pl3">CMMG-UI</h1>

    <TabGroup>
      <Tab title="Buttons" class="px3 py2">
        <Btn label="Button" class="btn-primary" />
        <TagGroup v-model="tags" :options="options" class="m2" />
        <ButtonGroup v-model="tag" :options="options" />
        <Pager v-model="page" :records="150" class="m2" />
        <Toggle v-model="active" label="A toggle" />
        <Checkbox v-model="active" label="A checkbox" />
        <RadioGroup v-model="tag" :options="options" />
      </Tab>
      <Tab title="Containers" class="px3 py2">
        <Btn label="Modal" class="btn-primary" @click="modal = true" />
        <Modal v-if="modal" @hide="modal = false">
          I'm a modal<br>
        </Modal>

        <Spinner active class="my2">
          Blurred text
        </Spinner>
        <Card title="Card">
          This is a card
        </Card>
        <Accordion title="Accordion">The content</Accordion>
        <Accordion title="Accordion">The content</Accordion>
        <Accordion title="Accordion">The content</Accordion>
      </Tab>
      <Tab title="Inputs" class="px3 py2">
        <InputBox v-model="text" label="Name" />
      </Tab>
      <Tab title="Complex Inputs" class="px3">
      </Tab>
      <Tab title="Other" class="px3 py2">
        <Btn label="Hover me" class="btn-primary" data-tip="Tooltip!" />
        <Progress value="50" />
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
      };
    },
  };
</script>
