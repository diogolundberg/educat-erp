<template>
  <div>
    <div class="my2 right-align">
      <Btn
        label="Inserir"
        primary
        @click="add" />
    </div>
    <template v-for="item in value">
      <slot
        :item="item"
        :error="errorsFor(index)" />
    </template>
    <div
      v-if="!value.length"
      class="bg-silver thin rounded p2 center">
      Nenhum item! Clique em "Inserir" para adicionar um novo.
    </div>
  </div>
</template>

<script>
  export default {
    name: "Multi",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      errors: {
        type: Object,
        default: () => [],
      },
      errorKey: {
        type: String,
        default: "",
      },
      default: {
        type: Object,
        default: () => ({}),
      },
    },
    methods: {
      add() {
        const value = [...this.value, this.default];
        this.$emit("input", value);
      },
      errorsFor(index) {
        const errors = this.errors && this.errors[`${this.errorKey}[${index}]`];
        return errors || {};
      },
    },
  };
</script>
