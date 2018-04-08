<template>
  <div class="flex flex-wrap">
    <div
      v-for="(type, index) in types"
      :key="index"
      class="col-12 sm-col-6 p1">
      <Card
        :title="type.name">
        <BaseErrors
          :value="errorsFor(type.id)" />
        <UploadButton
          :prefix="`${ prefix }${ type.id }/`"
          :value="get(type.id)"
          :disabled="disabled"
          @input="set(type.id, $event)" />
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
        type: Array,
        default: () => [],
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
      get(id) {
        const document = this.value.find(a => a.documentTypeId === id);
        return document && document.url;
      },
      set(documentTypeId, url) {
        const value = [...this.value];
        const document = value.find(a => a.documentTypeId === documentTypeId);
        if (document) {
          document.id = 0;
          document.url = url;
          this.$emit("input", value);
        } else {
          this.$emit("input", [...value, { url, documentTypeId }]);
        }
      },
      errorsFor(index) {
        const errors = this.errors && this.errors[index];
        return errors || [];
      },
    },
  };
</script>
