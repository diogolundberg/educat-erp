<template>
  <div class="flex flex-wrap">
    <div
      v-for="(type, index) in types"
      :key="index"
      class="col-12 sm-col-6 p1">
      <Card
        :title="type.name">
        <div
          slot="title"
          class="right">
          <UploadButton
            :prefix="`${ prefix }${ type.id }/`"
            :disabled="disabled"
            @input="push(type.id, $event)" />
        </div>
        <BaseErrors
          :value="errorsFor(type.id)" />
        <div
          v-for="(doc, index2) in docsFor(type)"
          :key="`${index}_${index2}`"
          class="mb1 pointer"
          @click="view(doc)">
          {{ type.name }}
          <Icon
            black
            name="download" />
        </div>
      </Card>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Documents",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      types: {
        type: Array,
        default: () => [],
      },
      errors: {
        type: Object,
        default: () => ({}),
      },
      prefix: {
        type: String,
        default: "",
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      push(documentTypeId, url) {
        const value = [...this.value];
        this.$emit("input", [...value, { url, documentTypeId }]);
      },
      view({ url }) {
        window.open(url, "_blank");
      },
      docsFor({ id }) {
        return this.value.filter(a => a.documentTypeId === id);
      },
      errorsFor(index) {
        const errors = this.errors && this.errors[index];
        return errors || [];
      },
    },
  };
</script>
