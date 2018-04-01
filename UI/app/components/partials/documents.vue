<template>
  <div class="p2 shadow0 rounded">
    <template v-for="(type, idx) in types">
      <div
        :id="`doc_${type.id}`"
        :key="type.id"
        class="flex justify-between items-center">
          <div>{{ type.name }}</div>
          <UploadButton
            :prefix="`${ prefix }${ type.id }/`"
            :value="get(type.id)"
            :disabled="disabled"
            @input="set(type.id, $event)" />
      </div>
      <hr
        v-if="idx < types.length - 1"
        :key="`hr${type.id}`">
    </template>
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
    },
  };
</script>
