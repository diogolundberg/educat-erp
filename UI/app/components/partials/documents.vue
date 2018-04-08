<template>
  <div class="flex flex-wrap">
    <div
      v-for="(type, index) in types"
      :key="index"
      class="col-12 sm-col-6 p1">
      <Card
        :title="type.name"
        no-padding>
        <div
          slot="title"
          class="mb3n right-align">
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
          class="py1 px2 divider-bottom h-bg-silver flex pointer"
          @click="modalUrl = doc.url">
          <div class="flex-auto">
            {{ type.name }} - {{ index2 + 1 }}
          </div>
          <Icon
            black
            name="download" />
        </div>
        <div
          v-if="docsFor(type).length == 0"
          class="py1 px2 divider-bottom">
            {{ emptyMessage }}
          </div>
      </Card>
    </div>
    <Modal
      v-if="!!modalUrl"
      class="fit-height"
      @hide="modalUrl = null">
      <img
        v-if="!!modalUrl.match('(jpe?g|png)$')"
        :src="modalUrl"
        class="col-12 fit">
      <iframe
        v-else
        :src="modalUrl"
        height="600"
        class="col-12 fit border-none" />
    </Modal>
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
      emptyMessage: {
        type: String,
        default: "Sem documentos. Clique no botão acima para fazer upload.",
      },
    },
    data() {
      return {
        modalUrl: null,
      };
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
