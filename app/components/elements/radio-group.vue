<template>
  <span :is="container">
    <span
      v-for="option in options"
      :is="container"
      :key="option[idField]"
      class="pointer my2"
      role="radiogroup"
      @click="$emit('input', option[idField])">
      <span
        :aria-checked="option[idField] == value"
        class="inline-block align-middle x2 y2 p6x circle bg-white shadow0"
        role="radio">
        <template v-if="option[idField] == value">
          <div class="block w100 h100 circle bg-blue transition" />
        </template>
      </span>
      <label class="mx1 bold upcase pointer">
        {{ option[labelField] }}
      </label>
    </span>
  </span>
</template>

<script>
  export default {
    name: "RadioGroup",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>
