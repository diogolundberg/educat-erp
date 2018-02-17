<template>
  <div>
    <h1 class="pl3">CMMG-UI</h1>

    <TabGroup>
      <Tab title="Buttons" class="px3">
        <Btn label="Button" class="btn-primary my2" />
        <TagGroup v-model="tags" :options="options" class="m2" />
      </Tab>
      <Tab title="Containers" class="px3">
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
        tags: []
      };
    },
  };
</script>
