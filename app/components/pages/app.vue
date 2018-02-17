<template>
  <div>
    <h1 class="pl3">CMMG-UI</h1>

    <TabGroup>
      <Tab title="Buttons" class="px3 py2">
        <Btn label="Button" class="btn-primary" />
        <TagGroup v-model="tags" :options="options" class="m2" />
        <ButtonGroup v-model="tag" :options="options" />
      </Tab>
      <Tab title="Containers" class="px3 py2">
        <Btn label="Modal" class="btn-primary" @click="modal = true" />
        <Modal v-if="modal" @hide="modal = false">
          I'm a modal<br>
        </Modal>
      </Tab>
      <Tab title="Inputs" class="px3">
      </Tab>
      <Tab title="Complex Inputs" class="px3">
      </Tab>
      <Tab title="Other" class="px3">
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
      };
    },
  };
</script>
